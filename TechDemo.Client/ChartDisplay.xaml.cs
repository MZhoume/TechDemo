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
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using TechDemo.Interface.Client;
using FragmentNavigationEventArgs = FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs;
using NavigatingCancelEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs;
using NavigationEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs;

namespace TechDemo.Client
{
    /// <summary>
    /// ChartDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class ChartDisplay : UserControl,IContent
    {
        public ChartDisplay()
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
            modernTab.Layout = (TabLayout) Application.Current.Properties["Layout"];

            var count = (Application.Current.Properties["DataModels"] as List<ObservableCollection<AbsDataModel>>)?.Count;
            for (int i = 0; i < count; i++)
            {
                modernTab.Links.Add(new Link()
                {
                    DisplayName = $"Data Source {i.ToString()}".ToString(),
                    Source = new Uri($"/Chart.xaml#{i.ToString()}", UriKind.Relative)
                });
            }

            if (count>0)
            {
                modernTab.SelectedSource = new Uri($"/Chart.xaml#0", UriKind.Relative);
            }
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            modernTab.Links.Clear();
        }
    }
}
