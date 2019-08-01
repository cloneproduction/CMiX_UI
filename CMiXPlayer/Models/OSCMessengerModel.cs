using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiXPlayer.Models
{
    public class OSCMessengerModel
    {
        public OSCMessengerModel()
        {

        }

        public int Port { get; set; }
        public string Address { get; set; }
    }
}