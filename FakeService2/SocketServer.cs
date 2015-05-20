using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;
using TechDemo.Interface.Server;

namespace FakeService2
{
    public class SocketServer : ISocketServer
    {
        public byte[] GenerateBytes(IDataModel[] dataModels)
        {
            throw new NotImplementedException();
        }

        public bool IsStopIntended(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
