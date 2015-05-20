using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeService1;

namespace DBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var connstr = @"data source = D:\文档\Visual Studio 2015\Projects\TechDemo\TechDemo.Server\Mock1.db";
            var context = new DBContext(new SQLiteConnection(connstr));

            foreach (var m in from i in context.DataModel select i)
            {
                Console.WriteLine(m.rowid);
            }


            Console.ReadKey();
        }
    }
}
