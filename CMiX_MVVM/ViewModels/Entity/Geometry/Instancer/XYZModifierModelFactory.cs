using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class XYZModifierModelFactory
    {
        public static XYZModifierModel GetModel(this XYZModifier instance)
        {
            XYZModifierModel model = new XYZModifierModel();

            //model.Name = instance.Name;
            
            //model.X = instance.X.GetModel();
            //model.Y = instance.Y.GetModel();
            //model.Z = instance.Z.GetModel();

            return model;
        }

        public static void SetViewModel(this XYZModifier instance, XYZModifierModel model)
        {
            instance.Name = model.Name;
            instance.X.SetViewModel(model.X);
            instance.Y.SetViewModel(model.Y);
            instance.Z.SetViewModel(model.Z);
        }

        public static ScaleModifierModel GetModel(this ScaleModifier instance)
        {
            ScaleModifierModel model = new ScaleModifierModel();

            model.Name = instance.Name;
            model.Uniform = instance.Uniform.GetModel();
            model.X = instance.X.GetModel();
            model.Y = instance.Y.GetModel();
            model.Z = instance.Z.GetModel();

            return model;
        }

        public static void ScaleModifierModel(this ScaleModifier instance, ScaleModifierModel model)
        {
            instance.Name = model.Name;
            instance.Uniform.SetViewModel(model.Uniform);
            instance.X.SetViewModel(model.X);
            instance.Y.SetViewModel(model.Y);
            instance.Z.SetViewModel(model.Z);
        }
    }
}
