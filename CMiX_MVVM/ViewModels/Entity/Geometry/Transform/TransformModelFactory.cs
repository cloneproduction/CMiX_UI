using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class TransformModelFactory
    {
        public static TransformModel GetModel(this Transform instance)
        {
            TransformModel model = new TransformModel();
            model.TranslateModel = instance.Translate.GetModel();
            model.ScaleModel = instance.Scale.GetModel();
            model.RotationModel = instance.Rotation.GetModel();
            model.Is3D = instance.Is3D;
            return model;
        }

        public static void SetViewModel(this Transform instance, TransformModel model)
        {
            instance.Translate.SetViewModel(model.TranslateModel);
            instance.Scale.SetViewModel(model.ScaleModel);
            instance.Rotation.SetViewModel(model.RotationModel);
        }


        public static RotationModel GetModel(this Rotation instance)
        {
            RotationModel model = new RotationModel();
            model.X = (SliderModel)instance.X.GetModel();
            model.Y = (SliderModel)instance.Y.GetModel();
            model.Z = (SliderModel)instance.Z.GetModel();
            return model;
        }

        public static void SetViewModel(this Rotation instance, RotationModel model)
        {
            instance.X.SetViewModel(model.X);
            instance.Y.SetViewModel(model.Y);
            instance.Z.SetViewModel(model.Z);
        }


        public static ScaleModel GetModel(this Scale instance)
        {
            ScaleModel model = new ScaleModel();
            model.X = (SliderModel)instance.X.GetModel();
            model.Y = (SliderModel)instance.Y.GetModel();
            model.Z = (SliderModel)instance.Z.GetModel();
            model.Uniform = (SliderModel)instance.Uniform.GetModel();
            return model;
        }

        public static void SetViewModel(this Scale instance, ScaleModel model)
        {
            instance.X.SetViewModel(model.X);
            instance.Y.SetViewModel(model.Y);
            instance.Z.SetViewModel(model.Z);
            instance.Uniform.SetViewModel(model.Uniform);
        }


        public static TranslateModel GetModel(this Translate instance)
        {
            TranslateModel model = new TranslateModel();
            model.X = (SliderModel)instance.X.GetModel();
            model.Y = (SliderModel)instance.Y.GetModel();
            model.Z = (SliderModel)instance.Z.GetModel();
            return model;
        }

        public static void SetViewModel(this Translate instance, TranslateModel model)
        {
            instance.X.SetViewModel(model.X);
            instance.Y.SetViewModel(model.Y);
            instance.Z.SetViewModel(model.Z);
        }
    }
}