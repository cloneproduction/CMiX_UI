using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class RangeModelFactory
    {
        public static IRangeModel GetModel(this Range instance)
        {
            IRangeModel model = new RangeModel();
            model.Minimum = instance.Minimum;
            model.Maximum = instance.Maximum;
            return model;
        }

        public static void SetViewModel(this Range instance, IRangeModel model)
        {
            instance.Minimum = model.Minimum;
            instance.Maximum = model.Maximum;
        }
    }
}
