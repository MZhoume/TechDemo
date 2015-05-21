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
    public class DataModel : TechDemo.Interface.Client.DataModel
    {
        private byte i;

        public int I { get; set; }

        public int II { get; set; }

        public int III { get; set; }

        public override byte[] ToBytes()
        {
            return new[] { i++, i, (byte)(i + 1) };
        }

        public override void Parse(byte[] bytes)
        {
            I = bytes[0];
            II = bytes[1];
            III = bytes[2];
        }

        public override int ServerID { get; set; }

        public override Dictionary<string, double> ValuesToDraw => 
            new Dictionary<string, double>()
        {
            ["I"]=I,
            ["II"]=II,
            ["III"]=III,
        };
    }
}
