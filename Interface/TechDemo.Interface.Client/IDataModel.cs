using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDemo.Interface.Client
{
    public interface IDataModel
    {
        byte[] ToBytes();

        void Parse(byte[] bytes);

        int ServerID { get; set; }

        Dictionary<string, double> ValuesToDraw { get; } 
    }
}
