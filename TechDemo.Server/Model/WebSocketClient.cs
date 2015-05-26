using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDemo.Server.Model
{
    public class WebSocketClient : IWebSocketClient 
    {
        public bool isStopIntended { get; set; }
    }
}
