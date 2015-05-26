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
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using TechDemo.Client.ViewModel;
using TechDemo.Interface.Client;
using FragmentNavigationEventArgs = FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs;
using NavigatingCancelEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs;
using NavigationEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs;

namespace TechDemo.Client
{
    /// <summary>
    /// DataDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class DataDisplay : UserControl, IContent
    {
        public DataDisplay()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            modernTab.Layout = (TabLayout)Application.Current.Properties["Layout"];
            var controls = (Application.Current.Resources["Locator"] as ViewModelLocator)?.Main.DisplayControls;
            var count = controls?.Count;

            for (int i = 0; i < count; i++)
            {
                modernTab.Links.Add(new Link()
                {
                    DisplayName = $"Server {i}",
                    Source = new Uri($"/DataControl.xaml#{i}", UriKind.Relative)
                });
            }

            if (count > 0)
            {
                modernTab.SelectedSource = modernTab.Links[0].Source;
            }
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            modernTab.Links.Clear();
            modernTab.SelectedSource = null;
        }
    }
}
