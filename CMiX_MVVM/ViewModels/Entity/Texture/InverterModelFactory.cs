using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class InverterModelFactory
    {
        public static InverterModel GetModel(this Inverter instance)
        {
            InverterModel model = new InverterModel();
            model.Invert = instance.Invert.GetModel();
            model.InvertMode = instance.InvertMode;
            return model;
        }

        public static void SetViewModel(this Inverter instance, InverterModel model)
        {
            instance.Invert.SetViewModel(model.Invert);
            instance.InvertMode = model.InvertMode;
        }
    }
}
