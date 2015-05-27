using System;

namespace TechDemo.Interface.Client
{
    public interface ISocketClient
    {
        event Action<AbsDataModel[]> DataReceived;

        byte[] GetResponseBytes();

        void Parse(byte[] bytes);
    }
}
