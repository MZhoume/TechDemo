using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TechDemo.Interface.Client;

namespace FakeService1
{
    public class SocketClient : ISocketClient
    {
        public SocketClient()
        {
            var timer = new Timer(1000);
            timer.Elapsed += (sender, args) =>
             {
                 Parse(null);
             };
            timer.Start();
        }

        private int i;

        public event Action<TechDemo.Interface.Client.AbsDataModel[]> DataReceived;
        public byte[] GetResponseBytes()
        {
            return new[] { (byte) 0x00 };
        }

        public void Parse(byte[] bytes)
        {
            DataReceived?.Invoke(new TechDemo.Interface.Client.AbsDataModel[]
            {
                new DataModel(0) { Values = new double[] { 0, 1, 2 }},
                new DataModel(1) { Values = new double[] { 0, 1, 2 }},
            });
        }
    }
}
