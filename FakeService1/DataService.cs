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
    public class DataService : TechDemo.Interface.Server.DataService
    {
        public DataService(int id, string portName)
            :base(id)
        {
            MessageBox.Show($"Creating service at {portName}");

            var timer = new Timer(3000);
            timer.Elapsed += Timer_Elapsed;

            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DataArrived?.Invoke(new DataModel() {I = 100, ServerID = ID});
        }

        public override event Action<TechDemo.Interface.Client.DataModel> DataArrived;
    }
}
