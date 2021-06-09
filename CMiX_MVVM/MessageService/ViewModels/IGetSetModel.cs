using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public interface IGetSetModel<T>
    {
        void SetViewModel(T model);
        T GetModel();
    }
}
