using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    public class LFOModel : IAnimModeModel
    {
        public LFOModel()
        {

        }

        public bool Invert { get; set; }
        public bool Enabled { get; set; }
    }
}
