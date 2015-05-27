using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace TempDect
{
    public class DBContext : TechDemo.Interface.Server.AbsDBContext
    {
        private readonly object _lock = new object();

        public DBContext(DbConnection conn) : base(conn)
        {}

        public DbSet<DataModel> DataModels { get; set; }

        public override void AddData(AbsDataModel data)
        {
            lock (_lock)
            {
                DataModels.Add(data as DataModel);
            }
        }

        public override Task<int> SaveChangesAsync()
        {
            lock (_lock)
            {
                return base.SaveChangesAsync();
            }
        }
    }
}
