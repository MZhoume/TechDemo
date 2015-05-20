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
using FirstFloor.ModernUI.Windows;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Common.Auxiliary;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using TechDemo.Interface.Client;
using FragmentNavigationEventArgs = FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs;
using NavigatingCancelEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs;
using NavigationEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs;

namespace TechDemo.Client
{
    /// <summary>
    /// Chart.xaml 的交互逻辑
    /// </summary>
    public partial class Chart : UserControl,IContent
    {
        public Chart()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            var num = int.Parse(e.Fragment);
            var list = (Application.Current.Properties["DataModels"] as List<List<IDataModel>>)[num];

            var model = list[0].ValuesToDraw;
            var dataCount = model.Count;
            var keys = new string[dataCount];

            model.Keys.CopyTo(keys, 0);
            var graphs = new LineGraph[dataCount];
            var points = new Point[dataCount][];
            var time = new EnumerableDataSource<int>(Enumerable.Range(0,list.Count));

            for (int i = 0; i < dataCount; i++)
            {
                points[i] = new Point[list.Count];
            }

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < dataCount; j++)
                {
                    points[j][i] = new Point(i, list[i].ValuesToDraw[keys[j]]);
                }
            }

            for (int i = 0; i < dataCount; i++)
            {
                var i1 = i;
                var ds = new CompositeDataSource(time, new EnumerableDataSource<Point>(points[i1]));
                graphs[i1] = new LineGraph()
                {
                    Description = new PenDescription(keys[i1]),
                    DataSource = ds,
                };

                plotter.AddChild(graphs[i1]);
            }
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}
