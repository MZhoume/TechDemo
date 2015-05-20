using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using TechDemo.Interface.Client;

namespace TechDemo.Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<GenericMessage<List<ObservableCollection<IDataModel>>>>(this, (msg) =>
            {
                Application.Current.Properties["DataModels"] = msg.Content;
            });
        }
    }
}
