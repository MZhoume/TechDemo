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
        public byte[] GenerateBytes(TechDemo.Interface.Client.AbsDataModel[] dataModels)
        {
            return new byte[] { 0x00 };
        }

        public bool IsStopIntended(byte[] data)
        {
            return data[0] == 0xff;
        }
    }
}
