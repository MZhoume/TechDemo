using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace TempDect
{
    public class DataService : TechDemo.Interface.Server.AbsDataService, IDisposable
    {
        private SerialPort _serialPort;

        public DataService(int id, string com) : base(id)
        {
            _serialPort = new SerialPort(com);
            _serialPort.DataReceived += (sender, args) =>
            {
                if (args.EventType == SerialData.Eof)
                {
                    var b = new byte[_serialPort.BytesToRead];
                    _serialPort.Read(b, 0, _serialPort.BytesToRead);
                    var dm = new DataModel(id);
                    dm.Parse(b);
;                    DataArrived?.Invoke(dm);
                }
            };

            _serialPort.Open();
        }

        public override event Action<AbsDataModel> DataArrived;

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
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
