using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public interface IComponentFactory
    {
        IComponent CreateComponent(IComponent parentComponent);
    }
}
