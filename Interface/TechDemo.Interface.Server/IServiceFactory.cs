using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDemo.Interface.Server
{
    public interface IServiceFactory
    {
        DataService CreateService(int id, string str);
    }
}
