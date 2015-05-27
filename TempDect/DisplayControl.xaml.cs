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

namespace TempDect
{
    /// <summary>
    /// DataControl.xaml 的交互逻辑
    /// </summary>
    public partial class DisplayControl : AbsDisplayControl
    {
        public DisplayControl()
        {
            InitializeComponent();
        }

        public override void PopulateData(AbsDataModel data)
        {
            var dat = data as DataModel;
            tempBar.Value = dat.Temperature;
            lblGas.Content = dat.Gas;
            lblLight.Content = dat.Light;
            rbLamp.IsChecked = dat.Lamp > 0;
            rbAlert.IsChecked = dat.Alert > 0;
        }
    }
}
