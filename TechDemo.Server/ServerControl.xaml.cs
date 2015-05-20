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
using GalaSoft.MvvmLight.Messaging;

namespace TechDemo.Server
{
    /// <summary>
    /// ServerControl.xaml 的交互逻辑
    /// </summary>
    public partial class ServerControl : UserControl
    {
        public ServerControl()
        {
            InitializeComponent();

            Messenger.Default.Register<NotificationMessage<int>>("Init UI", (m) =>
            {
                // TODO: Init uis as data received and binding to viewmodel
            });

            Messenger.Default.Register<string>("Stopped", (m) =>
            {
                //TODO: clear all views
            });
        }
    }
}
