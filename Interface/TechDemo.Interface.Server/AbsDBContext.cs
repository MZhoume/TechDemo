using System.Data.Common;
using System.Data.Entity;
using TechDemo.Interface.Client;

namespace TechDemo.Interface.Server
{
    // ReSharper disable once InconsistentNaming
    public abstract class AbsDBContext : DbContext
    {
        protected AbsDBContext(DbConnection conn)
            : base(conn, true)
        {
        }

        public abstract void AddData(AbsDataModel data);
    }
}
