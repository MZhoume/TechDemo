using System;
using TechDemo.Interface.Client;

namespace TechDemo.Interface.Server
{
    public abstract class AbsDataService
    {
        protected AbsDataService(int id)
        {
            ID = id;
        }

        public abstract event Action<AbsDataModel> DataArrived;

        // ReSharper disable once InconsistentNaming
        protected int ID { get; private set; }
    }
}
