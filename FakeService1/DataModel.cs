using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace FakeService1
{
    public class DataModel : IDataModel
    {
        private byte i;

        [Key]
        public long rowid { get; set; }

        public int I { get; set; }

        public int II { get; set; }

        public int III { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public byte[] ToBytes()
        {
            return new[] { i++, i, (byte)(i + 1) };
        }

        public void Parse(byte[] bytes)
        {
            I = bytes[0];
            II = bytes[1];
            III = bytes[2];
        }

        public int ServerID { get; set; }
    }
}
