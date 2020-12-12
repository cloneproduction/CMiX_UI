using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public interface IModifier
    {
        AnimParameter X { get; set; }
        AnimParameter Y { get; set; }
        AnimParameter Z { get; set; }
    }
}
