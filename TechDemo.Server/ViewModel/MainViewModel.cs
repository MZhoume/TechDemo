using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using TechDemo.Interface.Client;
using TechDemo.Interface.Server;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Fleck;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using TechDemo.Server.Model;

namespace TechDemo.Server.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly AbsDataService[] _dataServices;
        private readonly AbsDBContext _dbContext;
        private readonly ISocketServer _socketServer;
        private Socket _socket;
        private WebSocketServer _webSocket;

        private readonly List<AbsDataModel>[] _dataModels;

        private readonly List<Socket> _clients = new List<Socket>();
        private readonly List<IWebSocketConnection> _webClients = new List<IWebSocketConnection>(); 

        private bool _isListening;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            var serviceFactory = ServiceLocator.Current.GetInstance<IServiceFactory>();
            var strings = Properties.Settings.Default.ServiceCtorString.Split(',', ' ', ';');
            _dataServices = new AbsDataService[strings.Length];
            _dataModels = new List<AbsDataModel>[strings.Length];

            _dbContext = ServiceLocator.Current.GetInstance<AbsDBContext>();
            _socketServer = ServiceLocator.Current.GetInstance<ISocketServer>();

            for (var i = 0; i < strings.Length; i++)
            {
                var i1 = i;
                _dataServices[i1] = serviceFactory.CreateService(i1, strings[i1]);
                _dataServices[i1].DataArrived += _dataService_DataArrived;

                _dataModels[i1] = new List<AbsDataModel>();
            }
        }

        private void _dataService_DataArrived(AbsDataModel obj)
        {
            _dataModels[obj.ServerID].Add(obj);

            _dbContext.AddData(obj);
            _dbContext.SaveChangesAsync();

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                ServerLogs.Add($"Got new info at {DateTime.Now.ToLongTimeString()} from server {obj.ServerID}");
            });

            if (!_isListening)
                return;

            SendToSocket();
            SendToWebSocket();
        }

        private void SendToWebSocket()
        {
            var res = JsonConvert.SerializeObject(_dataModels.Select(m => m.Last()).ToArray());

            foreach (var c in _webClients)
            {
                c.Send(res);
            }
        }

        private void SendToSocket()
        {
            for (int i = 0; i < _clients.Count; i++)
            {
                byte[] b;
                if (_clients[i].Available == 0)
                {
                    b = new byte[100];
                    try
                    {
                        _clients[i].Receive(b);
                    }
                    catch
                    {
                        _clients[i].Disconnect(false);
                        _clients[i].Close();
                        _clients[i].Dispose();

                        _clients.Remove(_clients[i]);
                        i--;

                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                            ServerLogs.Add("Client has disconnected."));
                        continue;
                    }
                }
                else
                {
                    b = new byte[_clients[i].Available];
                    _clients[i].Receive(b);
                }

                DispatcherHelper.CheckBeginInvokeOnUI(() => { ServerLogs.Add($"Received -- {b[0]}"); });

                var buf = _socketServer.GenerateBytes(_dataModels.Select(d => d.Last()).ToArray());
                _clients[i].BeginSend(buf, 0, buf.Length, SocketFlags.None, SendCallback, _clients[i]);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            var soc = ar.AsyncState as Socket;
            soc.EndSend(ar);

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                ServerLogs.Add($"Send data"));
        }

        private RelayCommand _startListenCommand;

        /// <summary>
        /// Gets the StartListenCommand.
        /// </summary>
        public RelayCommand StartListenCommand
        {
            get
            {
                return _startListenCommand
                    ?? (_startListenCommand = new RelayCommand(
                        () =>
                        {
                            if (!_startListenCommand.CanExecute(null))
                            {
                                return;
                            }

                            if (_isListening)
                            {
                                _isListening = false;

                                _clients.Clear();
                                _webClients.Clear();

                                _webSocket.Dispose();
                                _socket.Close();
                                _socket.Dispose();

                                ListenBtnString = "Start Listening";
                            }
                            else
                            {
                                _isListening = true;

                                var intro = new
                                {
                                    introduction = Properties.Settings.Default.Introduction,
                                    directive = ""
                                };

                                var res = JsonConvert.SerializeObject(intro);

                                var webPort = (int.Parse(Port) + 80).ToString();
                                _webSocket = new WebSocketServer("ws://" + IPAddress + ":" + webPort);

                                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                    ServerLogs.Add($"WebSocket starts on {DateTime.Now.ToShortTimeString()}"));

                                _webSocket.Start(c =>
                                {
                                    c.OnOpen = () =>
                                    {
                                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                            ServerLogs.Add("A web client has connected."));

                                        c.Send(res);
                                        _webClients.Add(c);
                                    };

                                    c.OnClose = () =>
                                    {
                                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                            ServerLogs.Add("A web client has disconnected."));
                                        _webClients.Remove(c);
                                    };

                                    c.OnError = ex =>
                                    {
                                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                            ServerLogs.Add(ex.Message));
                                        c.Close();
                                    };

                                    c.OnPing = bytes => c.SendPong(bytes);
                                    c.OnPong = bytes => c.SendPing(bytes);

                                    c.OnMessage = s =>
                                    {
                                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                            ServerLogs.Add($"Got websocket message at {DateTime.Now.ToShortTimeString()}"));

                                        var req = JsonConvert.DeserializeObject(s, typeof(WebSocketClient)) as WebSocketClient;
                                        if (req.isStopIntended)
                                        {
                                            c.Close();
                                        }
                                    };
                                });
                                
                                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                var ipAddr = System.Net.IPAddress.Parse(IPAddress);
                                var endPoint = new IPEndPoint(ipAddr, int.Parse(Port));

                                _socket.Bind(endPoint);
                                _socket.Listen(0);

                                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                    ServerLogs.Add($"Server starts listening on {endPoint.Address}:{endPoint.Port}"));

                                _socket.BeginAccept(AcceptCallback, null);

                                ListenBtnString = "Stop Listening";
                            }
                        }, () => !(string.IsNullOrEmpty(_ipAddress) || string.IsNullOrEmpty(_port))));
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                var c = _socket.EndAccept(ar);

                var intro = Encoding.UTF8.GetBytes(Properties.Settings.Default.Introduction);
                c.BeginSend(intro,0,intro.Length,SocketFlags.None, SendCallback, c);

                _clients.Add(c);
            }
            catch
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    ServerLogs.Add("Server stopped listening..."));

                return;
            }

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                ServerLogs.Add("Client has connected."));

            _socket.BeginAccept(AcceptCallback, null);
        }

        /// <summary>
        /// The <see cref="IPAddress" /> property's name.
        /// </summary>
        public const string IPAddressPropertyName = "IPAddress";

        private string _ipAddress = "";

        /// <summary>
        /// Sets and gets the IPAddress property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string IPAddress
        {
            get
            {
                return _ipAddress;
            }
            set
            {
                Set(() => IPAddress, ref _ipAddress, value);
            }
        }

        /// <summary>
        /// The <see cref="Port" /> property's name.
        /// </summary>
        public const string PortPropertyName = "Port";

        private string _port = "";

        /// <summary>
        /// Sets and gets the Port property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Port
        {
            get
            {
                return _port;
            }
            set
            {
                Set(() => Port, ref _port, value);
            }
        }

        /// <summary>
        /// The <see cref="ServerLogs" /> property's name.
        /// </summary>
        public const string ServerLogsPropertyName = "ServerLogs";

        private ObservableCollection<string> _serverLogs = new ObservableCollection<string>();

        /// <summary>
        /// Sets and gets the ServerLogs property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<string> ServerLogs
        {
            get
            {
                return _serverLogs;
            }
            set
            {
                Set(() => ServerLogs, ref _serverLogs, value);
            }
        }

        /// <summary>
        /// The <see cref="ListenBtnString" /> property's name.
        /// </summary>
        public const string ListenBtnStringPropertyName = "ListenBtnString";

        private string _listenBtnString = "Start Listening";

        /// <summary>
        /// Sets and gets the ListenBtnString property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ListenBtnString
        {
            get
            {
                return _listenBtnString;
            }
            set
            {
                Set(() => ListenBtnString, ref _listenBtnString, value);
            }
        }

        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                if (columnName == IPAddressPropertyName)
                {
                    return string.IsNullOrEmpty(_ipAddress) ? "Required" : null;
                }
                if (columnName == PortPropertyName)
                {
                    return string.IsNullOrEmpty(_port) ? "Required" : null;
                }
                return null;
            }
        }

        public override void Cleanup()
        {
            base.Cleanup();

            _socket.Close();
        }
    }
}