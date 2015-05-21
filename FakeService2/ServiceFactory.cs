using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Server;

namespace FakeService2
{
    public class ServiceFactory : IServiceFactory
    {
        public TechDemo.Interface.Server.DataService CreateService(int id, string str)
        {
            return new DataService(id, str);
        }
    }
}
