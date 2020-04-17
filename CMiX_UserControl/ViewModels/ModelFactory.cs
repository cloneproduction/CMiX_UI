using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using System;
using CMiX.ColorPicker.ViewModels;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public static class ModelFactory
    {
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

        public static void SetViewModel(this Component instance, IComponentModel componentModel)
        {
            if (instance is Project)
                ((Project)instance).SetViewModel(componentModel);

            else if (instance is Composition)
                ((Composition)instance).SetViewModel(componentModel);

            else if (instance is Layer)
                ((Layer)instance).SetViewModel(componentModel);

            else if (instance is Mask)
                ((Mask)instance).SetViewModel(componentModel);

            else if (instance is Scene)
                ((Scene)instance).SetViewModel(componentModel);

            else if (instance is Entity)
                ((Entity)instance).SetViewModel(componentModel);
        }


        public static ProjectModel GetModel(this Project instance)
        {
            ProjectModel projectModel = new ProjectModel();

            projectModel.ID = instance.ID;
            projectModel.MessageAddress = instance.MessageAddress;
            projectModel.Name = instance.Name;
            projectModel.IsVisible = instance.IsVisible;
            //projectModel.AssetManagerModel = instance.AssetManager.GetModel();

            foreach (Component component in instance.Components)
                projectModel.ComponentModels.Add(component.GetModel());

            return projectModel;
        }

        public static void SetViewModel(this Project instance, IComponentModel componentModel)
        {
            var projectModel = componentModel as ProjectModel;

            instance.ID = projectModel.ID;
            instance.Name = projectModel.Name;
            instance.IsVisible = projectModel.IsVisible;

            instance.Components.Clear();
            foreach (CompositionModel compositionModel in projectModel.ComponentModels)
            {
                Composition composition = new Composition(0, instance.MessageAddress, instance.Beat, new MessageService(), instance.Mementor);
                composition.SetViewModel(compositionModel);
                instance.AddComponent(composition);
            }

            //instance.AssetManager.SetViewModel(projectModel.AssetManagerModel);
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

        public static void SetViewModel(this Composition instance, IComponentModel model)
        {
            instance.MessageService.Disable();
            var compositionModel = model as CompositionModel;

            instance.Name = compositionModel.Name;
            instance.IsVisible = compositionModel.IsVisible;
            instance.ID = compositionModel.ID;

            instance.Beat.SetViewModel(compositionModel.BeatModel);
            instance.Camera.SetViewModel(compositionModel.CameraModel);
            instance.Transition.SetViewModel(compositionModel.TransitionModel);

            instance.Components.Clear();
            foreach (LayerModel componentModel in model.ComponentModels)
            {
                Layer layer = new Layer(0, instance.Beat, instance.MessageAddress, instance.MessageService, instance.Mementor);
                layer.SetViewModel(componentModel);
                instance.AddComponent(layer);
            }
            instance.MessageService.Enable();
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

        public static void SetViewModel(this Layer instance, IComponentModel model)
        {
            instance.MessageService.Disable();

            var layerModel = model as LayerModel;

            instance.Name = layerModel.Name;
            instance.Out = layerModel.Out;
            instance.ID = layerModel.ID;

            instance.Fade.SetViewModel(layerModel.Fade);
            instance.BlendMode.SetViewModel(layerModel.BlendMode);
            instance.PostFX.SetViewModel(layerModel.PostFXModel);

            instance.Components.Clear();
            foreach (var componentModel in layerModel.ComponentModels)
            {
                Component component = null;

                if (componentModel is SceneModel)
                    component = new Scene(0, instance.Beat, instance.MessageAddress, instance.MessageService, instance.Mementor);

                else if (componentModel is MaskModel)
                    component = new Mask(0, instance.Beat, instance.MessageAddress, instance.MessageService, instance.Mementor);

                if (component != null)
                {
                    component.SetViewModel(componentModel);
                    instance.AddComponent(component);
                }
            }

            instance.MessageService.Enable();
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

        public static void SetViewModel(this Scene instance, IComponentModel componentModel)
        {
            var sceneModel = componentModel as SceneModel;
            instance.MessageService.Disable();

            instance.Enabled = sceneModel.Enabled;
            instance.BeatModifier.SetViewModel(sceneModel.BeatModifierModel);
            instance.PostFX.SetViewModel(sceneModel.PostFXModel);

            instance.Components.Clear();
            foreach (EntityModel entityModel in sceneModel.ComponentModels)
            {
                Entity entity = new Entity(0, instance.Beat, instance.MessageAddress, instance.MessageService, instance.Mementor);
                entity.SetViewModel(entityModel);
                instance.AddComponent(entity);
            }

            instance.MessageService.Enable();
        }


        public static MaskModel GetModel(this Mask instance)
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

        public static void SetViewModel(this Mask instance, IComponentModel model)
        {
            var maskModel = model as MaskModel;
            instance.MessageService.Disable();

            instance.Enabled = maskModel.Enable;
            instance.MaskType = maskModel.MaskType;
            instance.MaskControlType = maskModel.MaskControlType;

            instance.BeatModifier.SetViewModel(maskModel.BeatModifierModel);
            instance.Texture.SetViewModel(maskModel.TextureModel);
            instance.Geometry.SetViewModel(maskModel.GeometryModel);
            instance.PostFX.SetViewModel(maskModel.PostFXModel);

            instance.MessageService.Enable();
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

        public static void SetViewModel(this Entity instance, IComponentModel componentModel)
        {
            instance.MessageService.Disable();

            var entityModel = componentModel as EntityModel;
            instance.Enabled = entityModel.Enabled;
            instance.Name = entityModel.Name;

            instance.BeatModifier.SetViewModel(entityModel.BeatModifierModel);
            instance.Texture.SetViewModel(entityModel.TextureModel);
            instance.Geometry.SetViewModel(entityModel.GeometryModel);
            instance.Coloration.SetViewModel(entityModel.ColorationModel);

            instance.MessageService.Enable();
        }




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


        public static AssetManagerModel GetModel(this AssetManager instance)
        {
            AssetManagerModel assetsModel = new AssetManagerModel();

            foreach (var asset in instance.Assets)
                assetsModel.AssetModels.Add(asset.GetModel());

            foreach (var asset in instance.FlattenAssets)
                assetsModel.FlattenAssetModels.Add(asset.GetModel());

            return assetsModel;
        }

        public static void SetViewModel(this AssetManager instance, AssetManagerModel model)
        {
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

            instance.FlattenAssets.Clear();
            foreach (var assetModel in model.FlattenAssetModels)
            {
                IAssets asset = null;

                if (assetModel is AssetGeometryModel)
                    asset = new AssetGeometry();

                else if (assetModel is AssetTextureModel)
                    asset = new AssetTexture();

                if (asset != null)
                {
                    asset.SetViewModel(assetModel);
                    instance.FlattenAssets.Add(asset);
                }
            }
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
            instance.MessageService.Disable();

            instance.Rotation = cameraModel.Rotation;
            instance.LookAt = cameraModel.LookAt;
            instance.View = cameraModel.View;

            instance.BeatModifier.SetViewModel(cameraModel.BeatModifierModel);
            instance.FOV.SetViewModel(cameraModel.FOV);
            instance.Zoom.SetViewModel(cameraModel.Zoom);

            instance.MessageService.Enable();
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
            instance.MessageService.Disable();

            instance.Range.SetViewModel(rangeControlModel.Range);
            instance.Modifier = rangeControlModel.Modifier;

            instance.MessageService.Enable();
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
            instance.MessageService.Disable();
            instance.ColorPicker.Paste(colorSelectorModel.ColorPickerModel);
            instance.MessageService.Enable();
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

        public static void SetViewModel(this Texture instance, TextureModel model)
        {
            instance.AssetPathSelector.SetViewModel(model.AssetPathSelectorModel);
            instance.Brightness.SetViewModel(model.Brightness);
            instance.Contrast.SetViewModel(model.Contrast);
            instance.Saturation.SetViewModel(model.Saturation);
            instance.Luminosity.SetViewModel(model.Luminosity);
            instance.Hue.SetViewModel(model.Hue);
            instance.Pan.SetViewModel(model.Pan);
            instance.Tilt.SetViewModel(model.Tilt);
            instance.Scale.SetViewModel(model.Scale);
            instance.Rotate.SetViewModel(model.Rotate);
            instance.Keying.SetViewModel(model.Keying);
            instance.Invert.SetViewModel(model.Invert);
            instance.InvertMode = model.InvertMode;
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
            instance.MessageService.Disable();

            //instance.Rotation.Paste(model.Rotation);
            //instance.Translate.Sa(model.RotationX);
            //instance.Paste(model.RotationY);
            //instance.RotationZ.Paste(model.RotationZ);

            instance.MessageService.Enable();
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
            instance.MessageService.Disable();

            instance.Scale.Paste(model.Scale);
            instance.ScaleX.Paste(model.ScaleX);
            instance.ScaleY.Paste(model.ScaleY);
            instance.ScaleZ.Paste(model.ScaleZ);

            instance.MessageService.Enable();
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
            instance.MessageService.Disable();

            instance.Translate.Paste(model.Translate);
            instance.TranslateX.Paste(model.TranslateX);
            instance.TranslateY.Paste(model.TranslateY);
            instance.TranslateZ.Paste(model.TranslateZ);

            instance.MessageService.Enable();
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
            instance.MessageService.Disable();

            instance.Slider.SetViewModel(model.Slider);
            instance.BeatModifier.SetViewModel(model.BeatModifier);

            instance.MessageService.Enable();
        }


        public static CounterModel GetModel(this Counter instance)
        {
            CounterModel model = new CounterModel();
            model.Count = instance.Count;
            return model;
        }

        public static void SetViewModel(this Counter instance, CounterModel model)
        {
            instance.MessageService.Enable();
            instance.Count = model.Count;
            instance.MessageService.Enable();
        }
    }
}
