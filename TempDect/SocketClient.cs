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
            var len = bytes[0];
            var models = new AbsDataModel[len];
            for (var i = 0; i < len; i++)
            {
                var from = i * 33 + 1;
                models[i] = new DataModel(bytes[from + 0])
                {
                    Temperature = BitConverter.ToDouble(bytes, from + 1),
                    Gas = BitConverter.ToDouble(bytes, from + 9),
                    Light = BitConverter.ToDouble(bytes, from + +17),
                    Lamp = BitConverter.ToInt32(bytes, from + 25),
                    Alert = BitConverter.ToInt32(bytes, from + 29),
                };
            }

            DataReceived?.Invoke(models);
        }
    }
}
