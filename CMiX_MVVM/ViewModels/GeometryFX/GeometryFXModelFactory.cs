using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class GeometryFXModelFactory
    {
        public static GeometryFXModel GetModel(this GeometryFX instance)
        {
            GeometryFXModel geometryFXModel = new GeometryFXModel();
            geometryFXModel.Explode = (SliderModel)instance.Explode.GetModel();
            return geometryFXModel;
        }

        public static void SetViewModel(this GeometryFX instance, GeometryFXModel model)
        {
            instance.Explode.SetViewModel(model.Explode);
        }
    }
}
