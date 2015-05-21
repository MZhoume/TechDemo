using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using TechDemo.Interface.Client;
using TechDemo.Interface.Server;

namespace FakeService2
{
    public class DataService : TechDemo.Interface.Server.AbsDataService
    {
        public DataService(int id, string portname)
            :base(id)
        {
            MessageBox.Show($"Port name is {portname} and thie is from service 2");

            var timer = new Timer(3000);
            timer.Elapsed += Timer_Elapsed;

            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DataArrived?.Invoke(new DataModel() { A = 200, ServerID = ID });
        }

        public override event Action<TechDemo.Interface.Client.AbsDataModel> DataArrived;
    }
}
