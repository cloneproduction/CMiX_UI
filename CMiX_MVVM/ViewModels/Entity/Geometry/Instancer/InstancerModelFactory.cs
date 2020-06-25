using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class InstancerModelFactory
    {
        public static InstancerModel GetModel(this Instancer instance)
        {
            InstancerModel model = new InstancerModel();
            model.Transform = instance.Transform.GetModel();
            model.Counter = instance.Counter.GetModel();
            //model.TranslateModifier = instance.TranslateModifier.GetModel();
            //model.ScaleModifier = instance.ScaleModifier.GetModel();
            //model.RotationModifier = instance.RotationModifier.GetModel();
            //model.NoAspectRatio = instance.NoAspectRatio;
            return model;
        }

        public static void SetViewModel(this Instancer instance, InstancerModel model)
        {
            instance.Transform.SetViewModel(model.Transform);
            instance.Counter.SetViewModel(model.Counter);
            //instance.TranslateModifier.SetViewModel(model.TranslateModifier);
            //instance.ScaleModifier.SetViewModel(model.ScaleModifier);
            //instance.RotationModifier.SetViewModel(model.RotationModifier);
            //instance.NoAspectRatio = model.NoAspectRatio;
        }
    }
}