using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public class ComponentEventArgs : EventArgs
    {
        public ComponentEventArgs(Component component)
        {
            Component = component;
        }

        public Component Component { get; set; }
    }
}
