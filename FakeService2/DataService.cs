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
    public class DataService : TechDemo.Interface.Server.DataService
    {
        private SerialPort _serialPort;

        public DataService(int id, string portname)
            :base(id)
        {
            _serialPort = new SerialPort(portname);
        }

        public override event Action<TechDemo.Interface.Client.AbsDataModel> DataArrived;
    }
}
