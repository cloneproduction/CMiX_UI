using CMiX.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public static class CounterModelFactory
    {
        public static CounterModel GetModel(this Counter instance)
        {
            CounterModel model = new CounterModel();
            model.Count = instance.Count;
            return model;
        }

        public static void SetViewModel(this Counter instance, CounterModel model)
        {
            instance.Count = model.Count;
        }
    }
}
