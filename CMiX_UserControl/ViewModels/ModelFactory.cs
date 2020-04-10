using CMiX.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public static class ModelFactory
    {
        public static CameraModel GetModel(this Camera instance)
        {
            CameraModel cameraModel = new CameraModel();

            cameraModel.Rotation = instance.Rotation;
            cameraModel.LookAt = instance.LookAt;
            cameraModel.View = instance.View;

            return cameraModel;
        }

        public static CompositionModel GetModel(this Composition instance)
        {
            CompositionModel compositionModel = new CompositionModel();
            compositionModel.Name = instance.Name;
            compositionModel.IsVisible = instance.IsVisible;
            compositionModel.ID = instance.ID;

            compositionModel.CameraModel = instance.Camera.GetModel();
            compositionModel.MasterBeatModel = instance.Beat.GetModel();
            compositionModel.TransitionModel = instance.Transition.GetModel();

            foreach (Component component in instance.Components)
                compositionModel.ComponentModels.Add(component.GetModel());

            return compositionModel;
        }
        public static GeometryModel GetModel(this Geometry instance)
        {
            GeometryModel model = new GeometryModel();

            model.TransformModel = instance.Transform.GetModel();
            model.GeometryFXModel = instance.GeometryFX.GetModel();
            model.InstancerModel = instance.Instancer.GetModel();
            model.AssetPathSelectorModel = instance.AssetPathSelector.GetModel();

            return model;
        }

        public static GeometryFXModel GetModel(this GeometryFX instance)
        {
            GeometryFXModel geometryFXModel = new GeometryFXModel();
            geometryFXModel.Explode = instance.Explode.GetModel();
            return geometryFXModel;
        }
        public static InstancerModel GetModel(this Instancer instance)
        {
            InstancerModel model = new InstancerModel();

            model.Transform = instance.Transform.GetModel();
            model.Counter = instance.Counter.GetModel();
            model.TranslateModifier = instance.TranslateModifier.GetModel();
            model.ScaleModifier = instance.ScaleModifier.GetModel();
            model.RotationModifier = instance.RotationModifier.GetModel();
            model.NoAspectRatio = instance.NoAspectRatio;

            return model;
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

        public static ScaleModifierModel GetModel(this ScaleModifier instance)
        {
            ScaleModifierModel scaleModifierModel = new ScaleModifierModel();

            scaleModifierModel.Scale = instance.Scale.GetModel();
            scaleModifierModel.ScaleX = instance.ScaleX.GetModel();
            scaleModifierModel.ScaleY = instance.ScaleY.GetModel();
            scaleModifierModel.ScaleZ = instance.ScaleZ.GetModel();

            return scaleModifierModel;
        }
        public static TranslateModifierModel GetModel(this TranslateModifier instance)
        {
            TranslateModifierModel translateModifierModel = new TranslateModifierModel();

            translateModifierModel.Translate = instance.Translate.GetModel();
            translateModifierModel.TranslateX = instance.TranslateX.GetModel();
            translateModifierModel.TranslateY = instance.TranslateY.GetModel();
            translateModifierModel.TranslateZ = instance.TranslateZ.GetModel();

            return translateModifierModel;
        }

        public static AnimParameterModel GetModel(this AnimParameter instance)
        {
            AnimParameterModel animParameterModel = new AnimParameterModel();
            animParameterModel.Slider = instance.Slider.GetModel();
            animParameterModel.BeatModifier = instance.BeatModifier.GetModel();
            return animParameterModel;
        }

        public static AssetPathSelectorModel GetModel(this AssetPathSelector<T> instance)
        {
            AssetPathSelectorModel model = new AssetPathSelectorModel();

            if (SelectedPath != null)
                model.SelectedPath = this.SelectedPath;

            return model;
        }

        public static AssetManagerModel GetModel(this AssetMananger instance)
        {
            AssetManagerModel assetsModel = new AssetManagerModel();

            foreach (var asset in instance.Assets)
                assetsModel.AssetModels.Add(asset.GetModel());

            foreach (var asset in instance.FlattenAssets)
                assetsModel.FlattenAssetModels.Add(asset.GetModel());

            return assetsModel;
        }
        public static TranslateModel GetModel(this Translate instance)
        {
            TranslateModel translateModel = new TranslateModel();
            translateModel.TranslateX = instance.TranslateX.GetModel();
            translateModel.TranslateY = instance.TranslateY.GetModel();
            translateModel.TranslateZ = instance.TranslateZ.GetModel();
            return translateModel;
        }
        public static CounterModel GetModel(this Counter instance)
        {
            CounterModel counterModel = new CounterModel();
            counterModel.Count = instance.Count;
            return counterModel;
        }
        public static TransformModel GetModel(this Transform instance)
        {
            TransformModel model = new TransformModel();

            model.TranslateModel = instance.Translate.GetModel();
            model.ScaleModel = instance.Scale.GetModel();
            model.RotationModel = instance.Rotation.GetModel();
            model.Is3D = instance.Is3D;

            return model;
        }
    }
}
