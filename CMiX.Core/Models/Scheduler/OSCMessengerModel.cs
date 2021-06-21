using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Core.Models.Scheduler
{
    public class OSCSenderModel
    {
        public OSCSenderModel()
        {

        }

        public int Port { get; set; }
        public string Address { get; set; }
    }
}
