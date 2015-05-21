using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace DllValidator
{
    static class Validator
    {
        public static void ValidateClient(Assembly assembly)
        {
            var names = assembly.ExportedTypes.ToLookup(t => t.Name);
            var dataModel = names["DataModel"].First();
            var displayControl = names["DisplayControl"].First();
            var socketClient = names["SocketClient"].First();

            var dmi = Activator.CreateInstance(dataModel);
            var dci = Activator.CreateInstance(displayControl);
            var sci = Activator.CreateInstance(socketClient);
        }

        public static void ValidateServer(Assembly assembly)
        {
            var names = assembly.ExportedTypes.ToLookup(t => t.Name);
            var dataService = names["DataService"].First();
            var dbContext = names["DBContext"].First();
            var serviceFactory = names["ServiceFactory"].First();
            var socketServer = names["SocketServer"].First();

            var dsi = Activator.CreateInstance(dataService);
            var dbi = Activator.CreateInstance(dbContext);
            var sfi = Activator.CreateInstance(serviceFactory);
            var ssi = Activator.CreateInstance(socketServer);
        }
    }
}
