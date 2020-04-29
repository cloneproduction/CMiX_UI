using CMiX.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public static class SliderModelFactory
    {
        public static SliderModel GetModel(this Slider instance)
        {
            SliderModel model = new SliderModel();
            model.Amount = instance.Amount;
            return model;
        }

        public static void SetViewModel(this Slider instance, SliderModel model)
        {
            instance.Amount = model.Amount;
        }
    }
}
