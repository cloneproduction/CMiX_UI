using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class RangeModelFactory
    {
        public static RangeModel GetModel(this Range instance)
        {
            RangeModel model = new RangeModel();
            //model.Minimum = instance.Minimum;
            //model.Maximum = instance.Maximum;
            return model;
        }

        public static void SetViewModel(this Range instance, RangeModel model)
        {
            instance.Minimum = model.Minimum;
            instance.Maximum = model.Maximum;
        }
    }
}
