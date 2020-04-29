using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using CMiX.ColorPicker.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public static class ComponentModelFactory
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

            foreach (Component component in instance.Components)
                projectModel.ComponentModels.Add(component.GetModel());

            foreach (IAssets asset in instance.Assets)
                projectModel.AssetModels.Add(asset.GetModel());

            foreach (IAssets asset in instance.AssetsFlatten)
                projectModel.AssetModelsFlatten.Add(asset.GetModel());

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
                Composition composition = ComponentFactory.CreateComposition(instance);
                composition.SetViewModel(compositionModel);
                instance.AddComponent(composition);
            }

            instance.Assets.Clear();
            foreach (IAssetModel assetModel in projectModel.AssetModels)
            {
                IAssets asset = null;
                if (assetModel is AssetDirectoryModel)
                    asset = new AssetDirectory();
                else if (assetModel is AssetTextureModel)
                    asset = new AssetTexture();
                else if (assetModel is AssetGeometryModel)
                    asset = new AssetGeometry();

                asset.SetViewModel(assetModel);
                instance.Assets.Add(asset);
            }
            instance.BuildAssetFlattenCollection(instance.Assets);
        }


        public static CompositionModel GetModel(this Composition instance)
        {
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
            instance.MessengerService.Disable();
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
                Layer layer = ComponentFactory.CreateLayer(instance);
                layer.SetViewModel(componentModel);
                instance.AddComponent(layer);
            }
            instance.MessengerService.Enable();
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
            instance.MessengerService.Disable();

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
                    component = ComponentFactory.CreateScene(instance);

                else if (componentModel is MaskModel)
                    component = ComponentFactory.CreateMask(instance);

                if (component != null)
                {
                    component.SetViewModel(componentModel);
                    instance.AddComponent(component);
                }
            }

            instance.MessengerService.Enable();
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

            instance.Enabled = sceneModel.Enabled;
            instance.BeatModifier.SetViewModel(sceneModel.BeatModifierModel);
            instance.PostFX.SetViewModel(sceneModel.PostFXModel);

            instance.Components.Clear();
            foreach (EntityModel entityModel in sceneModel.ComponentModels)
            {
                Entity entity = ComponentFactory.CreateEntity(instance);
                entity.SetViewModel(entityModel);
                instance.AddComponent(entity);
            }
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

            instance.Enabled = maskModel.Enable;
            instance.MaskType = maskModel.MaskType;
            instance.MaskControlType = maskModel.MaskControlType;

            instance.BeatModifier.SetViewModel(maskModel.BeatModifierModel);
            instance.Texture.SetViewModel(maskModel.TextureModel);
            instance.Geometry.SetViewModel(maskModel.GeometryModel);
            instance.PostFX.SetViewModel(maskModel.PostFXModel);
            instance.Components.Clear();

            foreach (EntityModel entityModel in maskModel.ComponentModels)
            {
                Entity entity = ComponentFactory.CreateEntity(instance);
                entity.SetViewModel(entityModel);
                instance.AddComponent(entity);
            }
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
            instance.MessengerService.Disable();

            var entityModel = componentModel as EntityModel;
            instance.Enabled = entityModel.Enabled;
            instance.Name = entityModel.Name;

            instance.BeatModifier.SetViewModel(entityModel.BeatModifierModel);
            instance.Texture.SetViewModel(entityModel.TextureModel);
            instance.Geometry.SetViewModel(entityModel.GeometryModel);
            instance.Coloration.SetViewModel(entityModel.ColorationModel);

            instance.MessengerService.Enable();
        }
    }
}
