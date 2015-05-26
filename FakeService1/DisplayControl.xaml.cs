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

namespace FakeService1
{
    /// <summary>
    /// DisplayControl.xaml 的交互逻辑
    /// </summary>
    public partial class DisplayControl : AbsDisplayControl
    {
        public DisplayControl()
        {
            InitializeComponent();
        }

        public override void PopulateData(TechDemo.Interface.Client.AbsDataModel data)
        {
            var dat = data as DataModel;
            I.Text = dat.I.ToString();
            II.Text = dat.II.ToString();
            III.Text = dat.III.ToString();

            ID.Text = dat.ServerID.ToString();
        }
    }
}
