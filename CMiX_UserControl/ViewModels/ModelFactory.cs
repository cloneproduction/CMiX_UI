using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.ColorPicker.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public static class ModelFactory
    {
        public static IAssetModel GetModel(this IAssets instance)
        {
            if (instance is AssetDirectory)
                return ((AssetDirectory)instance).GetModel();
            else if (instance is AssetTexture)
                return ((AssetTexture)instance).GetModel();
            else if (instance is AssetGeometry)
                return ((AssetGeometry)instance).GetModel();
            else return null;
        }

        public static IAssetModel GetModel(this AssetDirectory instance)
        {
            IAssetModel directoryAssetModel = new AssetDirectoryModel();

            directoryAssetModel.Name = instance.Name;
            foreach (var asset in instance.Assets)
            {
                directoryAssetModel.AssetModels.Add(asset.GetModel());
            }
            return directoryAssetModel;
        }


        public static IAssetModel GetModel(this AssetGeometry instance)
        {
            IAssetModel assetModel = new AssetGeometryModel();

            assetModel.Name = instance.Name;
            assetModel.Path = instance.Path;
            assetModel.Ponderation = instance.Ponderation;

            return assetModel;
        }


        public static IAssetModel GetModel(this AssetTexture instance)
        {
            IAssetModel assetModel = new AssetTextureModel();

            assetModel.Name = instance.Name;
            assetModel.Path = instance.Path;
            assetModel.Ponderation = instance.Ponderation;

            return assetModel;
        }

        public static RangeControlModel GetModel(this RangeControl instance)
        {
            RangeControlModel rangeControlModel = new RangeControlModel();

            rangeControlModel.Range = instance.Range.GetModel();
            rangeControlModel.Modifier = instance.Modifier;

            return rangeControlModel;
        }

        public static CameraModel GetModel(this Camera instance)
        {
            CameraModel cameraModel = new CameraModel();

            cameraModel.Rotation = instance.Rotation;
            cameraModel.LookAt = instance.LookAt;
            cameraModel.View = instance.View;

            return cameraModel;
        }

        public static RotationModel GetModel(this Rotation instance)
        {
            RotationModel rotationModel = new RotationModel();

            rotationModel.RotationX = instance.RotationX.GetModel();
            rotationModel.RotationY = instance.RotationY.GetModel();
            rotationModel.RotationZ = instance.RotationZ.GetModel();

            return rotationModel;
        }


        public static EntityModel GetModel(this Entity instance)
        {
            EntityModel entityModel = new EntityModel();

            entityModel.Enabled = instance.Enabled;
            entityModel.Name = instance.Name;

            entityModel.BeatModifierModel = instance.BeatModifier.GetModel();
            entityModel.TextureModel = instance.Texture.GetModel();
            entityModel.GeometryModel = instance.Geometry.GetModel();
            entityModel.ColorationModel = instance.Coloration.GetModel();

            return entityModel;
        }


        public static SceneModel GetModel(this Scene instance)
        {
            SceneModel sceneModel = new SceneModel();

            sceneModel.Enabled = instance.Enabled;
            sceneModel.BeatModifierModel = instance.BeatModifier.GetModel();
            sceneModel.PostFXModel = instance.PostFX.GetModel();

            foreach (Entity item in instance.Components)
                sceneModel.ComponentModels.Add(item.GetModel());

            return sceneModel;
        }

        public static BlendModeModel GetModel(this BlendMode instance)
        {
            BlendModeModel blendModeModel = new BlendModeModel();

            blendModeModel.Mode = instance.Mode;

            return blendModeModel;
        }

        public static IComponentModel GetModel(this Mask instance)
        {
            MaskModel maskModel = new MaskModel();

            maskModel.Enable = instance.Enabled;
            maskModel.MaskType = instance.MaskType;
            maskModel.MaskControlType = instance.MaskControlType;

            maskModel.BeatModifierModel = instance.BeatModifier.GetModel();
            maskModel.TextureModel = instance.Texture.GetModel();
            maskModel.GeometryModel = instance.Geometry.GetModel();
            maskModel.PostFXModel = instance.PostFX.GetModel();

            foreach (Entity item in instance.Components)
                maskModel.ComponentModels.Add(item.GetModel());

            return maskModel;
        }

        public static IComponentModel GetModel(this Component instance)
        {
            if (instance is Project)
                return ((Project)instance).GetModel();

            else if (instance is Composition)
                return ((Composition)instance).GetModel();

            else if (instance is Layer)
                return ((Layer)instance).GetModel();

            else if (instance is Mask)
                return ((Mask)instance).GetModel();

            else if (instance is Scene)
                return ((Scene)instance).GetModel();

            else if (instance is Entity)
                return ((Entity)instance).GetModel();

            else return null;
        }


        public static ColorationModel GetModel(this Coloration instance)
        {
            ColorationModel colorationModel = new ColorationModel();

            colorationModel.ColorSelectorModel = instance.ColorSelector.GetModel();
            colorationModel.BeatModifierModel = instance.BeatModifier.GetModel();
            colorationModel.HueModel = instance.Hue.GetModel();
            colorationModel.SatModel = instance.Saturation.GetModel();
            colorationModel.ValModel = instance.Value.GetModel();

            return colorationModel;
        }


        public static ColorSelectorModel GetModel(this ColorSelector instance)
        {
            ColorSelectorModel colorSelectorModel = new ColorSelectorModel();

            colorSelectorModel.ColorPickerModel = instance.ColorPicker.GetModel();

            return colorSelectorModel;
        }


        public static PostFXModel GetModel(this PostFX instance)
        {
            PostFXModel postFXModel = new PostFXModel();

            postFXModel.Feedback = instance.Feedback.GetModel();
            postFXModel.Blur = instance.Blur.GetModel();
            postFXModel.Transforms = instance.Transforms;
            postFXModel.View = instance.View;

            return postFXModel;
        }


        public static LayerModel GetModel(this Layer instance)
        {
            LayerModel layerModel = new LayerModel();

            layerModel.Name = instance.Name;
            layerModel.ID = instance.ID;
            layerModel.Out = instance.Out;

            layerModel.Fade = instance.Fade.GetModel();
            layerModel.BlendMode = instance.BlendMode.GetModel();
            layerModel.PostFXModel = instance.PostFX.GetModel();

            foreach (var item in instance.Components)
                layerModel.ComponentModels.Add(item.GetModel());

            return layerModel;
        }


        public static BeatModel GetModel(this Beat instance)
        {
            BeatModel beatModel = new BeatModel();

            beatModel.Period = instance.Period;

            return beatModel;
        }

        public static BeatModifierModel GetModel(this BeatModifier instance)
        {
            BeatModifierModel beatModifierModel = new BeatModifierModel();

            beatModifierModel.ChanceToHit = instance.ChanceToHit.GetModel();
            beatModifierModel.Multiplier = instance.Multiplier;

            return beatModifierModel;
        }


        public static CompositionModel GetModel(this Composition instance)
        {
            Console.WriteLine("GET COMPOSITION MODEL");
            CompositionModel compositionModel = new CompositionModel();

            compositionModel.Name = instance.Name;
            compositionModel.IsVisible = instance.IsVisible;
            compositionModel.ID = instance.ID;

            compositionModel.CameraModel = instance.Camera.GetModel();
            compositionModel.BeatModel = instance.Beat.GetModel();
            compositionModel.TransitionModel = instance.Transition.GetModel();

            foreach (Layer component in instance.Components)
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


        public static ScaleModel GetModel(this Scale instance)
        {
            ScaleModel scaleModel = new ScaleModel();

            scaleModel.ScaleX = instance.ScaleX.GetModel();
            scaleModel.ScaleY = instance.ScaleY.GetModel();
            scaleModel.ScaleZ = instance.ScaleZ.GetModel();

            return scaleModel;
        }


        public static SliderModel GetModel(this Slider instance)
        {
            SliderModel sliderModel = new SliderModel();

            sliderModel.Amount = instance.Amount;

            return sliderModel;
        }

        public static ProjectModel GetModel(this Project instance)
        {
            ProjectModel projectModel = new ProjectModel();

            projectModel.ID = instance.ID;
            projectModel.MessageAddress = instance.MessageAddress;
            projectModel.Name = instance.Name;
            projectModel.IsVisible = instance.IsVisible;
            projectModel.AssetManagerModel = instance.AssetManager.GetModel();


            Console.WriteLine("GetProject Model");
            foreach (Component component in instance.Components)
                projectModel.ComponentModels.Add(component.GetModel());

            return projectModel;
        }

        public static TextureModel GetModel(this Texture instance)
        {
            TextureModel model = new TextureModel();

            model.AssetPathSelectorModel = instance.AssetPathSelector.GetModel();
            model.Brightness = instance.Brightness.GetModel();
            model.Contrast = instance.Contrast.GetModel();
            model.Saturation = instance.Saturation.GetModel();
            model.Luminosity = instance.Luminosity.GetModel();
            model.Hue = instance.Hue.GetModel();
            model.Pan = instance.Pan.GetModel();
            model.Tilt = instance.Tilt.GetModel();
            model.Scale = instance.Scale.GetModel();
            model.Rotate = instance.Rotate.GetModel();
            model.Keying = instance.Keying.GetModel();
            model.Invert = instance.Invert.GetModel();
            model.InvertMode = instance.InvertMode;

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
            TranslateModifierModel model = new TranslateModifierModel();

            model.Translate = instance.Translate.GetModel();
            model.TranslateX = instance.TranslateX.GetModel();
            model.TranslateY = instance.TranslateY.GetModel();
            model.TranslateZ = instance.TranslateZ.GetModel();

            return model;
        }


        public static AnimParameterModel GetModel(this AnimParameter instance)
        {
            AnimParameterModel model = new AnimParameterModel();

            model.Slider = instance.Slider.GetModel();
            model.BeatModifier = instance.BeatModifier.GetModel();

            return model;
        }


        public static AssetPathSelectorModel GetModel(this AssetPathSelector<AssetTexture> instance)
        {
            AssetPathSelectorModel model = new AssetPathSelectorModel();

            if (instance.SelectedPath != null)
                model.SelectedPath = instance.SelectedPath;

            return model;
        }


        public static AssetPathSelectorModel GetModel(this AssetPathSelector<AssetGeometry> instance)
        {
            AssetPathSelectorModel model = new AssetPathSelectorModel();

            if (instance.SelectedPath != null)
                model.SelectedPath = instance.SelectedPath;

            return model;
        }

        public static AssetManagerModel GetModel(this AssetManager instance)
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
            TranslateModel model = new TranslateModel();
            model.TranslateX = instance.TranslateX.GetModel();
            model.TranslateY = instance.TranslateY.GetModel();
            model.TranslateZ = instance.TranslateZ.GetModel();
            return model;
        }


        public static CounterModel GetModel(this Counter instance)
        {
            CounterModel model = new CounterModel();
            model.Count = instance.Count;
            return model;
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
