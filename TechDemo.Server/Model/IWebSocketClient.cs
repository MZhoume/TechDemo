using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDemo.Server.Model
{
    public interface IWebSocketClient
    {
        bool isStopIntended { get; set; }
    }
}
