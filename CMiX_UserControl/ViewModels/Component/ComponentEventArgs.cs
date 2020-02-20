using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public class ComponentEventArgs : EventArgs
    {
        public ComponentEventArgs(IComponent component)
        {
            Component = component;
        }

        public IComponent Component { get; set; }
    }
}
