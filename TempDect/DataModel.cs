using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempDect
{
    public enum DataType
    {
        ShakingHands = 0x80,
        Temperature = 0x81,
        Gas = 0x82,
        Light = 0x83,
        Lamp = 0x84,
        Alarm = 0x85,
        Done = 0x8F
    }

    public class DataModel : TechDemo.Interface.Client.AbsDataModel
    {
        private double _temperature;
        private double _gas;
        private double _light;

        private int _lamp;
        private int _alarm;

        public DataModel(int id) : base(id)
        {}

        public virtual byte[] ToBytes()
        {
            throw new NotImplementedException();
        }

        public virtual void Parse(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public override string[] Names => new[]
        {
            "Temperature",
            "Gas",
            "Light"
        };

        public override double[] Values => new[]
        {
            _temperature,
            _gas,
            _light
        };
    }
}
