using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.Observer
{
    public interface IObserver
    {
        void Update(int count);
    }
}
