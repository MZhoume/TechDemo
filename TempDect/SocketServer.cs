using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace TempDect
{
    public class SocketServer : TechDemo.Interface.Server.ISocketServer
    {
        public byte[] GenerateBytes(AbsDataModel[] dataModels)
        {
            throw new NotImplementedException();
        }
    }
}
