using CMiX.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    public class SteadyModel : IAnimModeModel
    {
        public SteadyModel()
        {

        }

        public SteadyType SteadyType { get; set; }
        public bool Enabled { get; set; }
    }
}
