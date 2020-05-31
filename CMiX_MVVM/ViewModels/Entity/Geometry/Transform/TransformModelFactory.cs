using CMiX.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            RotationModel rotationModel = new RotationModel();
            rotationModel.X = instance.X.GetModel();
            rotationModel.Y = instance.Y.GetModel();
            rotationModel.Y = instance.Y.GetModel();
            return rotationModel;
        }

        public static void SetViewModel(this Rotation instance, RotationModel model)
        {
            instance.X.SetViewModel(model.X);
            instance.Y.SetViewModel(model.Y);
            instance.Z.SetViewModel(model.Z);
        }


        public static ScaleModel GetModel(this Scale instance)
        {
            ScaleModel scaleModel = new ScaleModel();
            scaleModel.X = instance.X.GetModel();
            scaleModel.Y = instance.Y.GetModel();
            scaleModel.Z = instance.Z.GetModel();
            return scaleModel;
        }

        public static void SetViewModel(this Scale instance, ScaleModel model)
        {
            instance.X.SetViewModel(model.X);
            instance.Y.SetViewModel(model.Y);
            instance.Z.SetViewModel(model.Z);
        }


        public static TranslateModel GetModel(this Translate instance)
        {
            TranslateModel model = new TranslateModel();
            model.X = instance.X.GetModel();
            model.Y = instance.Y.GetModel();
            model.Z = instance.Z.GetModel();
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
