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
                new DataModel(0) {Values = new double[] {10,11,12}},
                new DataModel(1) {Values = new double[] {10,11,12}},
                new DataModel(2) {Values = new double[] {10,11,12}},
            });
        }
    }
}
