using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Core.Presentation.ViewModels
{
    public class CounterEventArgs
    {
        public CounterEventArgs(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }
}
