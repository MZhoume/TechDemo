using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DllValidator
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Please select which dll type you want to validate:");
            Console.WriteLine("1. Client");
            Console.WriteLine("2. Server");
            var key = Console.ReadKey(true);

            while (key.KeyChar != '1' && key.KeyChar != '2')
            {
                Console.WriteLine("Please select again.");
                key = Console.ReadKey(true);
            }

            var type = key.KeyChar == '1' ? "Client" : "Server";

            Console.WriteLine($"Please enter the name of the {type} Dll:");
            var dll = Console.ReadLine();

            while (!File.Exists(dll + ".dll"))
            {
                Console.WriteLine($"{dll} doesn't exist, please try again:");
                dll = Console.ReadLine();
            }

            var connstr = "";
            if (type == "Server")
            {
                Console.WriteLine("Please enter database's name");
                connstr = Console.ReadLine();
            }

            try
            {
                var assembly = Assembly.Load(dll);

                if (type == "Client")
                    Validator.ValidateClient(assembly);
                else
                    Validator.ValidateServer(assembly, connstr);

                Console.WriteLine($"{dll} is valid!");
            }
            catch (Exception ex)
            {
                LogEx(ex);
                Console.WriteLine($"{dll} does not validate!");
            }

            Console.ReadKey(true);
        }

        private static void LogEx(Exception exception)
        {
            if (exception.InnerException!=null)
            {
                LogEx(exception.InnerException);
            }

            Console.WriteLine(exception.Message);
        }
    }
}
