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
using System.Threading;
using GalaSoft.MvvmLight.Threading;

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
        private readonly DataService[] _dataServices;
        private readonly DBContext _dbContext;
        private readonly ISocketServer _socketServer;

        private readonly List<IDataModel>[] _dataModels;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            var serviceFactory = ServiceLocator.Current.GetInstance<IServiceFactory>();
            var coms = Properties.Settings.Default.SerialPorts.Split(',', ' ', ';');
            _dataServices = new DataService[coms.Length];
            _dataModels = new List<IDataModel>[coms.Length];

            _dbContext = ServiceLocator.Current.GetInstance<DBContext>();
            _socketServer = ServiceLocator.Current.GetInstance<ISocketServer>();

            for (var i = 0; i < coms.Length; i++)
            {
                var i1 = i;
                _dataServices[i1] = serviceFactory.CreateService(i1, coms[i1]);
                _dataServices[i1].DataArrived += _dataService_DataArrived;

                _dataModels[i1] = new List<IDataModel>();
            }
        }

        private void _dataService_DataArrived(IDataModel obj)
        {
            _dataModels[obj.ServerID].Add(obj);

            _dbContext.AddData(obj);

            _dbContext.SaveChangesAsync();

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                ServerLogs.Add($"Got new info at {DateTime.Now.ToLongTimeString()} from server {obj.ServerID}");
            });
        }

        private bool _isListening;
        private Socket _socket;
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
                                _socket.Close();
                                ListenBtnString = "Start Listening";
                            }
                            else
                            {
                                Task.Factory.StartNew(() =>
                                {
                                    using (_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                                        ProtocolType.Tcp))
                                    {
                                        var ipAddr = System.Net.IPAddress.Parse(IPAddress);
                                        var endPoint = new IPEndPoint(ipAddr, int.Parse(Port));

                                        _socket.Bind(endPoint);
                                        _socket.Listen(0);

                                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                        {
                                            ServerLogs.Add($"Server starts listening on {endPoint.Address}:{endPoint.Port}");

                                        });

                                        while (true)
                                        {
                                            try
                                            {
                                                var c = _socket.Accept();
                                                Task.Factory.StartNew(() =>
                                                {
                                                    using (c)
                                                    {
                                                        var b = new byte[5];
                                                        while (_socket.Connected)
                                                        {
                                                            while (c.Available == 0)
                                                            { }
                                                            c.Receive(b);
                                                            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                                            {
                                                                ServerLogs.Add($"Received -- {b[0]}");
                                                            });

                                                            if (_socketServer.IsStopIntended(b))
                                                            {

                                                                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                                                {
                                                                    ServerLogs.Add("Ending...");
                                                                });
                                                                break;
                                                            }

                                                            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                                            {
                                                                ServerLogs.Add($"Send -- {b[0]}");
                                                            });
                                                            c.Send(_socketServer.GenerateBytes(_dataModels.Select(d => d.Last()).ToArray()));
                                                        }
                                                    }
                                                });
                                            }
                                            catch (Exception ex)
                                            {
                                                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                                {
                                                    ServerLogs.Add(ex.Message);
                                                    ServerLogs.Add(
                                                        $"Server stopped listening at {DateTime.Now.ToShortTimeString()}");
                                                });
                                                break;
                                            }
                                        }
                                    }
                                });

                                ListenBtnString = "Stop Listening";
                            }

                            _isListening = !_isListening;
                        }, () => !(string.IsNullOrEmpty(_ipAddress) || string.IsNullOrEmpty(_port))));
            }
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
                    return String.IsNullOrEmpty(_ipAddress) ? "Required" : null;
                }
                if (columnName == PortPropertyName)
                {
                    return String.IsNullOrEmpty(_port) ? "Required" : null;
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