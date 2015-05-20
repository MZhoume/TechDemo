using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace FakeService2
{
    public class DataModel : IDataModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }

        public void Parse(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public int ServerID { get; set; }
    }
}
