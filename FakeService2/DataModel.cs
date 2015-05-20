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
        public override byte[] ToBytes()
        {
            throw new NotImplementedException();
        }

        public override void Parse(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public override int ServerID { get; set; }

        public override Dictionary<string, double> ValuesToDraw { get; }
    }
}
