using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace FakeService2
{
    public class DataModel : TechDemo.Interface.Client.AbsDataModel
    {
        private byte i;

        public override byte[] ToBytes()
        {
            return new[] { i++, i, (byte)(i + 1) };
        }

        public override void Parse(byte[] bytes)
        {
            A = bytes[0];
            AA = bytes[1];
            AAA = bytes[2];

            Values = new double[]
            {
                A,AA,AAA,
            };
        }

        public override string[] Names { get; set; } = 
        {
            "A","AA","AAA"
        };

        public override double[] Values { get; set; }

        public int A { get; set; }

        public int AA { get; set; }

        public int AAA { get; set; }



        public DataModel(int id) : base(id)
        {}
    }
}
