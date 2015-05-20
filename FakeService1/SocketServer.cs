using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechDemo.Interface.Client;
using TechDemo.Interface.Server;

namespace FakeService1
{
    public class SocketServer : ISocketServer
    {
        public byte[] GenerateBytes(IDataModel[] dataModels)
        {
            return new byte[] { 0x00 };
        }

        public bool IsStopIntended(byte[] data)
        {
            Thread.Sleep(500);
            return data[0] == 0xff;
        }
    }
}
