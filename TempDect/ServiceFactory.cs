using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Server;

namespace TempDect
{
    public class ServiceFactory : TechDemo.Interface.Server.IServiceFactory
    {
        public AbsDataService CreateService(int id, string str)
        {
            return new DataService(id, str);
        }
    }
}
