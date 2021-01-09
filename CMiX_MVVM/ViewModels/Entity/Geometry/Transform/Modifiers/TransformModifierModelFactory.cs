using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class TransformModifierModelFactory
    {
        public static ITransformModifierModel GetModel(this ITransformModifier instance)
        {
            if (instance is RandomXYZ)
                return ((RandomXYZ)instance).GetModel();

            else return null;
        }


        public static void SetViewModel(this ITransformModifier instance, ITransformModifierModel model)
        {
            instance.Name = model.Name;
            instance.ID = model.ID;

            if (instance is RandomXYZ)
                ((RandomXYZ)instance).SetViewModel(model);
        }

        public static RandomXYZModel GetModel(this RandomXYZ instance)
        {
            RandomXYZModel model = new RandomXYZModel();
            model.ID = instance.ID;
            return model;
        }
    }
}