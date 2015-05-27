using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TechDemo.Interface.Client;

namespace TempDect
{
    public sealed class DataService : TechDemo.Interface.Server.AbsDataService, IDisposable
    {
        private readonly SerialPort _serialPort;
        private DataModel _dm;

        private int i;

        public DataService(int id, string com) : base(id)
        {
            //_serialPort = new SerialPort(com);
            //_serialPort.DataReceived += (sender, args) =>
            //{
            //    if (args.EventType == SerialData.Eof)
            //    {
            //        var b = new byte[_serialPort.BytesToRead];
            //        _serialPort.Read(b, 0, _serialPort.BytesToRead);

            //        if (_dm.Parse(b))
            //        {
            //            DataArrived?.Invoke(_dm);
            //            _dm = new DataModel(id);
            //        }
            //    }
            //};

            //_dm = new DataModel(id);

            //_serialPort.Open();

            var timer = new Timer(3000);
            timer.Elapsed += (s, e) =>
            {
                DataArrived?.Invoke(new DataModel(id)
                {
                    Temperature = i++,
                    Gas = i++,
                    Light = i++,
                    Lamp = i % 2,
                    Alert = i % 2 + 1,
                    Datetime = DateTime.Now.ToString("s")
                });
            };

            timer.Start();
        }

        public override event Action<AbsDataModel> DataArrived;

        #region IDisposable Support
        private bool disposedValue = false;

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _serialPort.Close();
                    _serialPort.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
