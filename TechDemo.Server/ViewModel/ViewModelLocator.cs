/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:TechDemo.Server"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using System.Data.SQLite;
using System.Reflection;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using TechDemo.Interface.Client;
using TechDemo.Interface.Server;
using TechDemo.Server.Properties;

namespace TechDemo.Server.ViewModel
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
                    var assemblyClient = Assembly.Load(Settings.Default.ClientAssemblyName);
                    SimpleIoc.Default.Register(() => assemblyClient.CreateInstance(Settings.Default.ClientAssemblyName+".DisplayControl") as IDisplayControl);
                    SimpleIoc.Default.Register(() => assemblyClient.CreateInstance(Settings.Default.ClientAssemblyName + ".DataModel") as AbsDataModel);
                    SimpleIoc.Default.Register(() => assemblyClient.CreateInstance(Settings.Default.ClientAssemblyName + ".SocketClient") as ISocketClient);

                    var assemblyServer = Assembly.Load(Settings.Default.ServerAssemblyName);
                    SimpleIoc.Default.Register(() => Activator.CreateInstance(assemblyServer.GetType(Settings.Default.ServerAssemblyName + ".DBContext"),
                        new SQLiteConnection(Settings.Default.ConnectionString)) as AbsDBContext);
                    SimpleIoc.Default.Register(() => assemblyServer.CreateInstance(Settings.Default.ServerAssemblyName + ".ServiceFactory") as IServiceFactory);
                    SimpleIoc.Default.Register(() => assemblyServer.CreateInstance(Settings.Default.ServerAssemblyName + ".SocketServer") as ISocketServer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Application init error - " + ex.Message);
                    throw;
                }
            }
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
            ServiceLocator.Current.GetInstance<MainViewModel>().Cleanup();
        }
    }
}