using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace FakeService2
{
    public class DBContext : TechDemo.Interface.Server.DBContext
    {
        public DBContext(DbConnection conn) : base(conn)
        { }

        public DbSet<DataModel> DataModels { get; set; }

        public override void AddData(TechDemo.Interface.Client.DataModel data)
        {
            DataModels.Add(data as DataModel);
        }
    }
}
