using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class InstancerModelFactory
    {
        public static InstancerModel GetModel(this Instancer instance)
        {
            InstancerModel model = new InstancerModel();
            model.Transform = instance.Transform.GetModel();
            model.NoAspectRatio = instance.NoAspectRatio;
            return model;
        }

        public static void SetViewModel(this Instancer instance, InstancerModel model)
        {
            instance.Transform.SetViewModel(model.Transform);
            instance.NoAspectRatio = model.NoAspectRatio;
        }
    }
}