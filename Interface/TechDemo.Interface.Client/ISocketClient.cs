using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDemo.Interface.Client
{
    public interface ISocketClient
    {
        event Action<IDataModel[]> DataReceived;

        byte[] GetResponseBytes(bool isStopIntended);

        void Parse(byte[] bytes);
    }
}
