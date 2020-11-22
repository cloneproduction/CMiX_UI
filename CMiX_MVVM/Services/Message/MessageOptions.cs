using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Services
{
    public class MessageOptions
    {
        public MessageOptions(object options)
        {
            Options = options;
        }

        public Object Options { get; set; }
    }
}
