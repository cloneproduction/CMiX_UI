using NetMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Services
{
    public class NetMQMessageEventArgs : EventArgs
    {
        public NetMQMessageEventArgs(NetMQMessage netMQMessage)
        {
            NetMQMessage = netMQMessage;
        }

        public NetMQMessage NetMQMessage { get; set; }
    }
}
