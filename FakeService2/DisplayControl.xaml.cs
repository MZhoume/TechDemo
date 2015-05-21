using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechDemo.Interface.Client;

namespace FakeService2
{
    /// <summary>
    /// DisplayControl.xaml 的交互逻辑
    /// </summary>
    public partial class DisplayControl : IDisplayControl
    {
        public DisplayControl()
        {
            InitializeComponent();
        }

        public void PopulateData(TechDemo.Interface.Client.AbsDataModel data)
        {
            var dat = data as DataModel;

            A.Content = dat.A;
            AA.Content = dat.AA;
            AAA.Content = dat.AAA;

            id.Content = dat.ServerID;
        }
    }
}
