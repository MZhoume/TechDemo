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
        private int i;

        public SocketClient()
        {
            var timer = new Timer(3000);
            timer.Elapsed += (sender, args) =>
             {
                 Parse(null);
             };

            timer.Start();
        }

        public event Action<IDataModel[]> DataReceived;
        public byte[] GetResponseBytes(bool isStopIntended)
        {
            return new[] { (byte)(isStopIntended ? 0xff : 0x00) };
        }

        public void Parse(byte[] bytes)
        {
            DataReceived?.Invoke(new IDataModel[] { new DataModel() { I =i,II = i,III = i++} });
        }
    }
}
