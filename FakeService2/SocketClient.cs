using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace FakeService2
{
    public class SocketClient : ISocketClient
    {
        public event Action<TechDemo.Interface.Client.AbsDataModel[]> DataReceived;
        public byte[] GetResponseBytes(bool isStopIntended)
        {
            throw new NotImplementedException();
        }

        public void Parse(byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}
