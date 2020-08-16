using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public interface IAnimMode
    {
        Range Range { get; set; }
        
        void Update();
    }
}
