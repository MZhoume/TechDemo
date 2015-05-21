using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;
using TechDemo.Interface.Server;

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

        public static void ValidateServer(Assembly assembly, string connstr)
        {
            var names = assembly.ExportedTypes.ToLookup(t => t.Name);
            var dbContext = names["DBContext"].First();
            var serviceFactory = names["ServiceFactory"].First();
            var socketServer = names["SocketServer"].First();
            
            var dbi = Activator.CreateInstance(dbContext, new SQLiteConnection(connstr));
            var sfi = Activator.CreateInstance(serviceFactory);
            var ssi = Activator.CreateInstance(socketServer);
            var dsi = (sfi as IServiceFactory).CreateService(0, "");

            (dsi as AbsDataService).DataArrived += null;

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo("sqlite3.exe", $"{connstr} .tables")
                { RedirectStandardOutput = true, UseShellExecute = false}
            };
            process.Start();
            if (string.IsNullOrEmpty(process.StandardOutput.ReadLine()))
            {
                throw new Exception("Database must have a table!");
            }
        }
    }
}
