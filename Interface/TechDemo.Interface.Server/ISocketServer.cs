using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace TechDemo.Interface.Server
{
    public interface ISocketServer
    {
        byte[] GenerateBytes(AbsDataModel[] dataModels);
    }
}
