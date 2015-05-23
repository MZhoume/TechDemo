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
        public byte[] GetResponseBytes()
        {
            return new [] { (byte) 0x00 };
        }

        public void Parse(byte[] bytes)
        {
            DataReceived?.Invoke(new []
            {
                new DataModel(0) {AA = 2},
                new DataModel(1) {AA = 2},
                new DataModel(2) {AA = 2},
            });
        }
    }
}
