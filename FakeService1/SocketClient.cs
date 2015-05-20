using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace FakeService1
{
    public class SocketClient : ISocketClient
    {
        public event Action<IDataModel[]> DataReceived;
        public byte[] GetResponseBytes(bool isStopIntended)
        {
            return new[] { (byte)(isStopIntended ? 0xff : 0x00) };
        }

        public void Parse(byte[] bytes)
        {
            DataReceived?.Invoke(new IDataModel[] { new DataModel() });
        }
    }
}
