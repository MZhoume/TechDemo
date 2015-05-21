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
    public class DataService : TechDemo.Interface.Server.AbsDataService
    {
        private int i;
        private List<DataModel> _models; 

        public DataService(int id, string portName)
            :base(id)
        {
            MessageBox.Show($"Creating service at {portName}");

            _models = new List<DataModel>() {new DataModel(ID) {I = 100} };
            var timer = new Timer(3000);
            timer.Elapsed += Timer_Elapsed;

            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DataArrived?.Invoke(new DataModel(ID) {I = 100});

            if (i++ == 5)
            {
                _models.Add(new DataModel(ID) {I = i*10});
                i = 0;
            }
        }

        public override event Action<TechDemo.Interface.Client.AbsDataModel> DataArrived;
    }
}
