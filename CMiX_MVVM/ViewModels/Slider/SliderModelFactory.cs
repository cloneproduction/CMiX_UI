﻿using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class SliderModelFactory
    {
        public static SliderModel GetModel(this Slider instance)
        {
            SliderModel model = new SliderModel();
            model.Amount = instance.Amount;
            model.Address = instance.GetAddress();
            return model;
        }

        public static void SetViewModel(this Slider instance, SliderModel model)
        {
            instance.Amount = model.Amount;
            //instance.Address = model.Address;
        }
    }
}