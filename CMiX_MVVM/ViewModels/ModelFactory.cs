using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class ModelFactory
    {
        public static AssetPathSelectorModel GetModel(this AssetPathSelector<AssetTexture> instance)
        {
            AssetPathSelectorModel model = new AssetPathSelectorModel();

            if (instance.SelectedPath != null)
                model.SelectedPath = instance.SelectedPath;

            return model;
        }

        public static void SetViewModel(this AssetPathSelector<AssetTexture> instance, AssetPathSelectorModel model)
        {
            if (model.SelectedPath != null)
                instance.SelectedPath = model.SelectedPath;
        }


        public static AssetPathSelectorModel GetModel(this AssetPathSelector<AssetGeometry> instance)
        {
            AssetPathSelectorModel model = new AssetPathSelectorModel();

            if (instance.SelectedPath != null)
                model.SelectedPath = instance.SelectedPath;

            return model;
        }

        public static void SetViewModel(this AssetPathSelector<AssetGeometry> instance, AssetPathSelectorModel model)
        {
            if (model.SelectedPath != null)
                instance.SelectedPath = model.SelectedPath;
        }



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

        public static void SetViewModel(this IAssets instance, IAssetModel model)
        {
            if (instance is AssetDirectory)
                ((AssetDirectory)instance).SetViewModel(model);
            else if (instance is AssetTexture)
                ((AssetTexture)instance).SetViewModel(model);
            else if (instance is AssetGeometry)
                ((AssetGeometry)instance).SetViewModel(model);
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

        public static void SetViewModel(this AssetDirectory instance, IAssetModel model)
        {
            instance.Name = model.Name;
            instance.Assets.Clear();
            foreach (var assetModel in model.AssetModels)
            {
                IAssets asset = null;

                if (assetModel is AssetDirectoryModel)
                    asset = new AssetDirectory();
                else if (assetModel is AssetGeometryModel)
                    asset = new AssetGeometry();
                else if (assetModel is AssetTextureModel)
                    asset = new AssetTexture();

                if (asset != null)
                {
                    asset.SetViewModel(assetModel);
                    instance.Assets.Add(asset);
                }
            }
            instance.SortAssets();
        }


        public static IAssetModel GetModel(this AssetGeometry instance)
        {
            IAssetModel assetModel = new AssetGeometryModel();

            assetModel.Name = instance.Name;
            assetModel.Path = instance.Path;
            assetModel.Ponderation = instance.Ponderation;

            return assetModel;
        }

        public static void SetViewModel(this AssetGeometry instance, IAssetModel assetModel)
        {
            instance.Name = assetModel.Name;
            instance.Path = assetModel.Path;
            instance.Ponderation = assetModel.Ponderation;
        }


        public static IAssetModel GetModel(this AssetTexture instance)
        {
            IAssetModel assetModel = new AssetTextureModel();

            assetModel.Name = instance.Name;
            assetModel.Path = instance.Path;
            assetModel.Ponderation = instance.Ponderation;

            return assetModel;
        }

        public static void SetViewModel(this AssetTexture instance, IAssetModel assetModel)
        {
            instance.Name = assetModel.Name;
            instance.Path = assetModel.Path;
            instance.Ponderation = assetModel.Ponderation;
        }



        public static CameraModel GetModel(this Camera instance)
        {
            CameraModel cameraModel = new CameraModel();

            cameraModel.Rotation = instance.Rotation;
            cameraModel.LookAt = instance.LookAt;
            cameraModel.View = instance.View;

            return cameraModel;
        }

        public static void SetViewModel(this Camera instance, CameraModel cameraModel)
        {
            instance.Rotation = cameraModel.Rotation;
            instance.LookAt = cameraModel.LookAt;
            instance.View = cameraModel.View;

            instance.BeatModifier.SetViewModel(cameraModel.BeatModifierModel);
            instance.FOV.SetViewModel(cameraModel.FOV);
            instance.Zoom.SetViewModel(cameraModel.Zoom);
        }


        public static RangeControlModel GetModel(this RangeControl instance)
        {
            RangeControlModel rangeControlModel = new RangeControlModel();

            rangeControlModel.Range = instance.Range.GetModel();
            rangeControlModel.Modifier = instance.Modifier;

            return rangeControlModel;
        }

        public static void SetViewModel(this RangeControl instance, RangeControlModel rangeControlModel)
        {
            instance.Range.SetViewModel(rangeControlModel.Range);
            instance.Modifier = rangeControlModel.Modifier;
        }


        public static BlendModeModel GetModel(this BlendMode instance)
        {
            BlendModeModel blendModeModel = new BlendModeModel();

            blendModeModel.Mode = instance.Mode;

            return blendModeModel;
        }

        public static void SetViewModel(this BlendMode instance, BlendModeModel blendModeModel)
        {
            instance.Mode = blendModeModel.Mode;
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

        public static void SetViewModel(this Coloration instance, ColorationModel colorationModel)
        {
            instance.ColorSelector.SetViewModel(colorationModel.ColorSelectorModel);
            instance.BeatModifier.SetViewModel(colorationModel.BeatModifierModel);
            instance.Hue.SetViewModel(colorationModel.HueModel);
            instance.Saturation.SetViewModel(colorationModel.SatModel);
            instance.Value.SetViewModel(colorationModel.ValModel);
        }


        public static ColorSelectorModel GetModel(this ColorSelector instance)
        {
            ColorSelectorModel colorSelectorModel = new ColorSelectorModel();

            colorSelectorModel.ColorPickerModel = instance.ColorPicker.GetModel();

            return colorSelectorModel;
        }

        public static void SetViewModel(this ColorSelector instance, ColorSelectorModel colorSelectorModel)
        {
            instance.ColorPicker.Paste(colorSelectorModel.ColorPickerModel);
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

        public static void SetViewModel(this PostFX instance, PostFXModel postFXmodel)
        {
            instance.Transforms = postFXmodel.Transforms;
            instance.View = postFXmodel.View;

            instance.Feedback.SetViewModel(postFXmodel.Feedback);
            instance.Blur.SetViewModel(postFXmodel.Blur);
        }


        public static BeatModel GetModel(this Beat instance)
        {
            BeatModel beatModel = new BeatModel();
            beatModel.Period = instance.Period;
            return beatModel;
        }

        public static void SetViewModel(this Beat instance, BeatModel beatModel)
        {
            instance.Period = beatModel.Period;
        }


        public static BeatModifierModel GetModel(this BeatModifier instance)
        {
            BeatModifierModel beatModifierModel = new BeatModifierModel();

            beatModifierModel.ChanceToHit = instance.ChanceToHit.GetModel();
            beatModifierModel.Multiplier = instance.Multiplier;

            return beatModifierModel;
        }

        public static void SetViewModel(this BeatModifier instance, BeatModifierModel beatModifierModel)
        {
            instance.Multiplier = beatModifierModel.Multiplier;
            instance.ChanceToHit.SetViewModel(beatModifierModel.ChanceToHit);
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

        public static void SetViewModel(this Geometry instance, GeometryModel model)
        {
            instance.Transform.SetViewModel(model.TransformModel);
            instance.GeometryFX.SetViewModel(model.GeometryFXModel);
            instance.Instancer.SetViewModel(model.InstancerModel);
            instance.AssetPathSelector.SetViewModel(model.AssetPathSelectorModel);
        }


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

        public static void SetViewModel(this Instancer instance, InstancerModel model)
        {
            instance.Transform.SetViewModel(model.Transform);
            instance.Counter.SetViewModel(model.Counter);
            instance.TranslateModifier.SetViewModel(model.TranslateModifier);
            instance.ScaleModifier.SetViewModel(model.ScaleModifier);
            instance.RotationModifier.SetViewModel(model.RotationModifier);
            instance.NoAspectRatio = model.NoAspectRatio;
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
            //instance.Rotation.Paste(model.Rotation);
            //instance.Translate.Sa(model.RotationX);
            //instance.Paste(model.RotationY);
            //instance.RotationZ.Paste(model.RotationZ);
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
            instance.Scale.Paste(model.Scale);
            instance.ScaleX.Paste(model.ScaleX);
            instance.ScaleY.Paste(model.ScaleY);
            instance.ScaleZ.Paste(model.ScaleZ);
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
            instance.Translate.Paste(model.Translate);
            instance.TranslateX.Paste(model.TranslateX);
            instance.TranslateY.Paste(model.TranslateY);
            instance.TranslateZ.Paste(model.TranslateZ);
        }


        public static AnimParameterModel GetModel(this AnimParameter instance)
        {
            AnimParameterModel model = new AnimParameterModel();

            model.Slider = instance.Slider.GetModel();
            model.BeatModifier = instance.BeatModifier.GetModel();

            return model;
        }

        public static void SetViewModel(this AnimParameter instance, AnimParameterModel model)
        {
            instance.Slider.SetViewModel(model.Slider);
            instance.BeatModifier.SetViewModel(model.BeatModifier);
        }


        public static CounterModel GetModel(this Counter instance)
        {
            CounterModel model = new CounterModel();
            model.Count = instance.Count;
            return model;
        }

        public static void SetViewModel(this Counter instance, CounterModel model)
        {
            instance.Count = model.Count;
        }
    }
}
