using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class GeometryModelFactory
    {
        public static GeometryModel GetModel(this Geometry instance)
        {
            GeometryModel model = new GeometryModel();
            model.TransformModel = instance.Transform.GetModel();
            model.GeometryFXModel = instance.GeometryFX.GetModel();
            model.InstancerModel = instance.Instancer.GetModel();
            model.AssetPathSelectorModel = instance.AssetPathSelector.GetModel();
            return model;
        }

        public static void SetViewModel(this Geometry instance, GeometryModel model)
        {
            instance.Transform.SetViewModel(model.TransformModel);
            instance.GeometryFX.SetViewModel(model.GeometryFXModel);
            instance.Instancer.SetViewModel(model.InstancerModel);
            instance.AssetPathSelector.SetViewModel(model.AssetPathSelectorModel);
        }
    }
}