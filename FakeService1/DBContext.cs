using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace FakeService1
{
    public class DBContext : TechDemo.Interface.Server.DBContext
    {
        private readonly object _lock = new object();

        public DBContext(DbConnection conn) : base(conn)
        { }

        public DbSet<DataModel> DataModel { get; set; }

        public override void AddData(TechDemo.Interface.Client.AbsDataModel data)
        {
            lock(_lock)
            {
                DataModel.Add(data as DataModel);
            }
        }

        public override Task<int> SaveChangesAsync()
        {
            Task<int> task;
            lock (_lock)
            {
                task = base.SaveChangesAsync();
            }
            return task;
        }
    }
}
