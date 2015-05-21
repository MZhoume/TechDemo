using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace TechDemo.Interface.Server
{
    // ReSharper disable once InconsistentNaming
    public abstract class DBContext : DbContext
    {
        public DBContext(DbConnection conn)
            : base(conn, true)
        {
        }

        public abstract void AddData(DataModel data);
    }
}
