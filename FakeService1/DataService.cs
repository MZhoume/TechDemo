using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using TechDemo.Interface.Client;
using TechDemo.Interface.Server;
using Timer = System.Timers.Timer;

namespace FakeService1
{
    public class DataService : IDataService
    {
        public DataService(string portname)
        {
            MessageBox.Show($"Creating service at {portname}");

            var timer = new Timer(10000);
            timer.Elapsed += Timer_Elapsed;

            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DataArrived?.Invoke(new DataModel() {I = 100});
        }

        public event Action<IDataModel> DataArrived;

        public int ID { get; set; }
    }
}
