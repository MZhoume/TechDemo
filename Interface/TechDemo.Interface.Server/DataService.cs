using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace TechDemo.Interface.Server
{
    public abstract class DataService
    {
        protected DataService(int id)
        {
            ID = id;
        }

        public abstract event Action<AbsDataModel> DataArrived;

        protected int ID { get; private set; }
    }
}
