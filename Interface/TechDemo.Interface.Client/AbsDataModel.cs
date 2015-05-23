using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDemo.Interface.Client
{
    public abstract class AbsDataModel
    {
        private AbsDataModel() { }

        protected AbsDataModel(int id)
        {
            ServerID = id;
        }

        [Key]
        public long rowid { get; set; }

        public abstract byte[] ToBytes();

        public abstract void Parse(byte[] bytes);

        public int ServerID { get; private set; }

        public abstract string[] Names { get; set; }

        public abstract double[] Values { get; set; }
    }
}
