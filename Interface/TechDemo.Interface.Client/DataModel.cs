using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDemo.Interface.Client
{
    public abstract class DataModel
    {
        [Key]
        public long rowid { get; set; }

        public abstract byte[] ToBytes();

        public abstract void Parse(byte[] bytes);

        public abstract int ServerID { get; set; }

        public abstract Dictionary<string, double> ValuesToDraw { get; } 
    }
}
