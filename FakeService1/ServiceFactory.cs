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
        public IDataService CreateService(params object[] objs)
        {
            return new DataService(objs[0] as string);
        }
    }
}
