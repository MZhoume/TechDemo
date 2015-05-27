using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace TempDect
{
    public class SocketClient : TechDemo.Interface.Client.ISocketClient
    {
        public event Action<AbsDataModel[]> DataReceived;
        public byte[] GetResponseBytes() => new byte[] {0x00};

        public void Parse(byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}
