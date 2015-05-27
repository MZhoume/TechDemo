using System.ComponentModel.DataAnnotations;

namespace TechDemo.Interface.Client
{
    public abstract class AbsDataModel
    {
        // ReSharper disable once UnusedMember.Local
        private AbsDataModel() { }

        protected AbsDataModel(int id)
        {
            ServerID = id;
        }

        [Key]
        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once InconsistentNaming
        public long rowid { get; set; }

        // ReSharper disable once InconsistentNaming
        public int ServerID { get; private set; }

        public abstract string[] Names { get; }

        public abstract double[] Values { get; }
    }
}
