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
            model.TranslateModifierModel = instance.TranslateModifier.GetModel();
            model.ScaleModifierModel = instance.ScaleModifier.GetModel();
            model.RotationModifierModel = instance.RotationModifier.GetModel();
            model.UniformScale = instance.UniformScale.GetModel();
            //model.NoAspectRatio = instance.NoAspectRatio;
            return model;
        }

        public static void SetViewModel(this Instancer instance, InstancerModel model)
        {
            instance.Transform.SetViewModel(model.Transform);
            instance.Counter.SetViewModel(model.Counter);
            instance.TranslateModifier.SetViewModel(model.TranslateModifierModel);
            instance.ScaleModifier.SetViewModel(model.ScaleModifierModel);
            instance.RotationModifier.SetViewModel(model.RotationModifierModel);
            instance.UniformScale.SetViewModel(model.UniformScale);
            //instance.NoAspectRatio = model.NoAspectRatio;
        }
    }
}