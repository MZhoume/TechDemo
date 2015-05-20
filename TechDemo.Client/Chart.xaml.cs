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
using FirstFloor.ModernUI.Windows;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
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
    public partial class Chart : UserControl, IContent
    {
        private ObservableDataSource<Point>[] _points;
        private int _dataCount;
        private string[] _keys;
        private int _time;
        private ObservableCollection<AbsDataModel> _list;
        private LineGraph[] _graphs = new LineGraph[0];

        public Chart()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            try
            {
                _list.CollectionChanged -= List_CollectionChanged;
            }
            catch
            {
                // ignored
            }

            foreach (var graph in _graphs)
            {
                plotter.Children.Remove(graph);
            }

            var num = int.Parse(e.Fragment);
            _list = (Application.Current.Properties["DataModels"] as List<ObservableCollection<AbsDataModel>>)[num];

            var model = _list[0].ValuesToDraw;
            _dataCount = model.Count;
            _keys = new string[_dataCount];

            _points = new ObservableDataSource<Point>[_dataCount];

            model.Keys.CopyTo(_keys, 0);
            var points = new Point[_dataCount][];

            for (int i = 0; i < _dataCount; i++)
            {
                points[i] = new Point[_list.Count];
            }

            for (_time = 0; _time < _list.Count; _time++)
            {
                for (int j = 0; j < _dataCount; j++)
                {
                    points[j][_time] = new Point(_time, _list[_time].ValuesToDraw[_keys[j]]);
                }
            }

            _graphs = new LineGraph[_dataCount];

            for (int i = 0; i < _dataCount; i++)
            {
                var i1 = i;

                _points[i1] = new ObservableDataSource<Point>(points[i1]);
                _graphs[i1] = plotter.AddLineGraph(_points[i1], _keys[i1]);
            }

            plotter.FitToView();

            _list.CollectionChanged += List_CollectionChanged;
        }

        private void List_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var data = e.NewItems[0] as AbsDataModel;
            for (int i = 0; i < _dataCount; i++)
            {
                var i1 = i;
                _points[i1].AppendAsync(DispatcherHelper.UIDispatcher, new Point(_time, data.ValuesToDraw[_keys[i1]]));
            }

            _time++;
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            _list.CollectionChanged -= List_CollectionChanged;
        }
    }
}
