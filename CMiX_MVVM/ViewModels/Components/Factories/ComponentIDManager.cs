using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public static class ComponentIDManager
    {
        private static int ID = 0;

        public static int GetNewID()
        {
            ID++;
            return ID;
        }
    }
}
