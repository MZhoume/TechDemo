using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        Alert = 0x85,
        Done = 0x8F
    }

    public class DataModel : TechDemo.Interface.Client.AbsDataModel
    {
        public double Temperature { get; set; }
        public double Gas { get; set; }
        public double Light { get; set; }

        public int Lamp { get; set; }
        public int Alert { get; set; }

        public string Datetime { get; set; }

        private int _parsedCount;

        public DataModel(int id) : base(id)
        {}

        public bool Parse(byte[] bytes)
        {
            if (bytes.Length != 5)
            {
                _parsedCount = 0;
                return false;
            }

            var low = bytes[3];
            var high = bytes[4];

            switch ((DataType)bytes[1])
            {
                case DataType.ShakingHands:
                    break;
                case DataType.Temperature:
                    var s = (high & 0x80) == 0x80 ? -1 : 1;
                    Temperature = s * (high + (int)(low * 0.0625));
                    _parsedCount++;
                    break;
                case DataType.Gas:
                    Gas = high;
                    _parsedCount++;
                    break;
                case DataType.Light:
                    Light = high;
                    _parsedCount++;
                    break;
                case DataType.Lamp:
                    var lid = bytes[2];
                    Lamp = lid;
                    _parsedCount++;
                    break;
                case DataType.Alert:
                    var aid = bytes[2];
                    Alert = aid;
                    _parsedCount++;
                    break;
                case DataType.Done:
                    Datetime = DateTime.Now.ToString("s");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _parsedCount == 5;
        }

        public override string[] Names => new[]
        {
            "Temperature",
            "Gas",
            "Light",
        };

        public override double[] Values => new[]
        {
            Temperature,
            Gas,
            Light
        };
    }
}
