using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;
using TechDemo.Interface.Server;

namespace FakeService2
{
    public class DataService : IDataService
    {
        private SerialPort _serialPort;

        public DataService(string portname)
        {
            _serialPort = new SerialPort(portname);
        }

        public event Action<IDataModel> DataArrived;

        public int ID { get; set; }
    }
}
