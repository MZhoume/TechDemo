using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Server;

namespace FakeService1
{
    public class ServiceFactory : IServiceFactory
    {
        public TechDemo.Interface.Server.DataService CreateService(int id, params object[] objs)
        {
            return new DataService(id, objs[0] as string);
        }
    }
}
