/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:TechDemo.Client"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using System.Reflection;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using TechDemo.Interface.Client;

namespace TechDemo.Client.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();

            if (!ViewModelBase.IsInDesignModeStatic)
            {
                try
                {
                    var assembly = Assembly.Load(Properties.Settings.Default.AssemblyName);
                    SimpleIoc.Default.Register(() => assembly.CreateInstance(Properties.Settings.Default.AssemblyName+".DisplayControl") as IDisplayControl);
                    SimpleIoc.Default.Register(() => assembly.CreateInstance(Properties.Settings.Default.AssemblyName + ".DataModel") as DataModel);
                    SimpleIoc.Default.Register(() => assembly.CreateInstance(Properties.Settings.Default.AssemblyName + ".SocketClient") as ISocketClient);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Application init error - " + ex.Message);
                    throw;
                }
            }

            Application.Current.Properties["Layout"] = TabLayout.Tab;
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}