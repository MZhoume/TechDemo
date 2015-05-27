using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Interface.Client;

namespace TempDect
{
    public class SocketServer : TechDemo.Interface.Server.ISocketServer
    {
        public byte[] GenerateBytes(AbsDataModel[] dataModels)
        {
            var models = dataModels.Select(m => m as DataModel).ToArray();

            var b = new byte[33 * models.Length + 1];
            b[0] = (byte)models.Length;
            for (var i = 0; i < models.Length; i++)
            {
                var from = i * 33 + 1;
                b[from] = (byte)models[i].ServerID;
                Array.Copy(BitConverter.GetBytes(models[i].Temperature), 0, b, from + 1, 8);
                Array.Copy(BitConverter.GetBytes(models[i].Gas), 0, b, from + 9, 8);
                Array.Copy(BitConverter.GetBytes(models[i].Light), 0, b, from + 17, 8);
                Array.Copy(BitConverter.GetBytes(models[i].Lamp), 0, b, from + 25, 4);
                Array.Copy(BitConverter.GetBytes(models[i].Alert), 0, b, from + 29, 4);
            }

            return b;
        }
    }
}
