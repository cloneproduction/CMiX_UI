using CMiX.MVVM.Models.Beat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public static class ResyncModelFactory
    {
        public static ResyncModel GetModel(this Resync instance)
        {
            ResyncModel model = new ResyncModel();

            return model;
        }

        public static void SetViewModel(this Resync instance, ResyncModel model)
        {
        }
    }
}
