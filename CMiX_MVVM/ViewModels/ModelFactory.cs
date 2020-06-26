using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class ModelFactory
    {
        public static GeometryFXModel GetModel(this GeometryFX instance)
        {
            GeometryFXModel geometryFXModel = new GeometryFXModel();
            geometryFXModel.Explode = instance.Explode.GetModel();
            return geometryFXModel;
        }

        public static void SetViewModel(this GeometryFX instance, GeometryFXModel model)
        {
            instance.Explode.SetViewModel(model.Explode);
        }

        public static RotationModifierModel GetModel(this RotationModifier instance)
        {
            RotationModifierModel rotationModifierModel = new RotationModifierModel();
            rotationModifierModel.Rotation = instance.Rotation.GetModel();
            rotationModifierModel.RotationX = instance.RotationX.GetModel();
            rotationModifierModel.RotationY = instance.RotationY.GetModel();
            rotationModifierModel.RotationZ = instance.RotationZ.GetModel();
            return rotationModifierModel;
        }

        public static void SetViewModel(this RotationModifier instance, RotationModifierModel model)
        {
            instance.Rotation.SetViewModel(model.Rotation);
            instance.RotationX.SetViewModel(model.RotationX);
            instance.RotationY.SetViewModel(model.RotationY);
            instance.RotationZ.SetViewModel(model.RotationZ);
        }


        public static ScaleModifierModel GetModel(this ScaleModifier instance)
        {
            ScaleModifierModel scaleModifierModel = new ScaleModifierModel();

            scaleModifierModel.Scale = instance.Scale.GetModel();
            scaleModifierModel.ScaleX = instance.ScaleX.GetModel();
            scaleModifierModel.ScaleY = instance.ScaleY.GetModel();
            scaleModifierModel.ScaleZ = instance.ScaleZ.GetModel();

            return scaleModifierModel;
        }

        public static void SetViewModel(this ScaleModifier instance, ScaleModifierModel model)
        {
            instance.Scale.SetViewModel(model.Scale);
            instance.ScaleX.SetViewModel(model.ScaleX);
            instance.ScaleY.SetViewModel(model.ScaleY);
            instance.ScaleZ.SetViewModel(model.ScaleZ);
        }


        public static TranslateModifierModel GetModel(this TranslateModifier instance)
        {
            TranslateModifierModel model = new TranslateModifierModel();

            model.Translate = instance.Translate.GetModel();
            model.TranslateX = instance.TranslateX.GetModel();
            model.TranslateY = instance.TranslateY.GetModel();
            model.TranslateZ = instance.TranslateZ.GetModel();

            return model;
        }

        public static void SetViewModel(this TranslateModifier instance, TranslateModifierModel model)
        {
            instance.Translate.SetViewModel(model.Translate);
            instance.TranslateX.SetViewModel(model.TranslateX);
            instance.TranslateY.SetViewModel(model.TranslateY);
            instance.TranslateZ.SetViewModel(model.TranslateZ);
        }
    }
}
