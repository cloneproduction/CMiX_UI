﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
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

            else if (instance is Scene)
                ((Scene)instance).SetViewModel(componentModel);

            else if (instance is Entity)
                ((Entity)instance).SetViewModel(componentModel);
        }


        private static ProjectModel GetModel(this Project instance)
        {
            ProjectModel projectModel = new ProjectModel();

            projectModel.Enabled = instance.Enabled;
            projectModel.ID = instance.ID;
            projectModel.Name = instance.Name;
            projectModel.IsVisible = instance.IsVisible;

            projectModel.Beat = instance.Beat.GetModel();

            foreach (Component component in instance.Components)
                projectModel.ComponentModels.Add(component.GetModel());
                
            foreach (IAssets asset in instance.Assets)
                projectModel.AssetModels.Add(asset.GetModel());

            //foreach (IAssets asset in instance.AssetsFlatten)
            //    projectModel.AssetModelsFlatten.Add(asset.GetModel());

            return projectModel;
        }

        private static void SetViewModel(this Project instance, IComponentModel componentModel)
        {
            var projectModel = componentModel as ProjectModel;

            instance.ID = projectModel.ID;
            instance.Name = projectModel.Name;
            instance.IsVisible = projectModel.IsVisible;

            instance.Beat.SetViewModel(projectModel.Beat);

            instance.Components.Clear();
            foreach (CompositionModel compositionModel in projectModel.ComponentModels)
            {
                Composition composition = ComponentFactory.CreateComposition(instance);
                composition.SetViewModel(compositionModel);
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
            //instance.BuildAssetFlattenCollection(instance.Assets);
        }


        private static CompositionModel GetModel(this Composition instance)
        {
            CompositionModel compositionModel = new CompositionModel();

            compositionModel.Name = instance.Name;
            compositionModel.IsVisible = instance.IsVisible;
            compositionModel.ID = instance.ID;

            compositionModel.BeatModel = instance.Beat.GetModel();
            compositionModel.CameraModel = instance.Camera.GetModel();
            compositionModel.TransitionModel = instance.Transition.GetModel();

            foreach (Layer component in instance.Components)
                compositionModel.ComponentModels.Add(component.GetModel());

            return compositionModel;
        }


        private static void SetViewModel(this Composition instance, IComponentModel model)
        {
            var compositionModel = model as CompositionModel;

            instance.Enabled = compositionModel.Enabled;
            instance.Name = compositionModel.Name;
            instance.ID = compositionModel.ID;
            instance.IsVisible = compositionModel.IsVisible;

            instance.Beat.SetViewModel(compositionModel.BeatModel);
            instance.Camera.SetViewModel(compositionModel.CameraModel);
            instance.Transition.SetViewModel(compositionModel.TransitionModel);

            instance.Components.Clear();
            foreach (LayerModel componentModel in model.ComponentModels)
            {
                Layer layer = ComponentFactory.CreateLayer(instance);
                layer.SetViewModel(componentModel);
            }
        }


        private static LayerModel GetModel(this Layer instance)
        {
            LayerModel layerModel = new LayerModel();

            layerModel.Enabled = instance.Enabled;
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


        private static void SetViewModel(this Layer instance, IComponentModel model)
        {
            var layerModel = model as LayerModel;

            instance.Enabled = layerModel.Enabled;
            instance.Name = layerModel.Name;
            instance.ID = layerModel.ID;
            instance.Out = layerModel.Out;
            
            instance.Fade.SetViewModel(layerModel.Fade);
            instance.BlendMode.SetViewModel(layerModel.BlendMode);
            instance.PostFX.SetViewModel(layerModel.PostFXModel);

            instance.Components.Clear();
            foreach (var componentModel in layerModel.ComponentModels)
            {
                Scene scene = ComponentFactory.CreateScene(instance);
                scene.SetViewModel(componentModel);
            }
        }


        private static SceneModel GetModel(this Scene instance)
        {
            SceneModel sceneModel = new SceneModel();

            sceneModel.Enabled = instance.Enabled;
            sceneModel.ID = instance.ID;
            sceneModel.Name = instance.Name;

            sceneModel.BeatModifierModel = instance.BeatModifier.GetModel();
            sceneModel.PostFXModel = instance.PostFX.GetModel();
            sceneModel.MaskModel = instance.Mask.GetModel();
            sceneModel.TransformModel = instance.Transform.GetModel();

            foreach (Entity item in instance.Components)
                sceneModel.ComponentModels.Add(item.GetModel());

            return sceneModel;
        }


        private static void SetViewModel(this Scene instance, IComponentModel componentModel)
        {
            var sceneModel = componentModel as SceneModel;

            instance.Enabled = sceneModel.Enabled;
            instance.ID = sceneModel.ID;
            instance.Name = sceneModel.Name;

            instance.BeatModifier.SetViewModel(sceneModel.BeatModifierModel);
            instance.PostFX.SetViewModel(sceneModel.PostFXModel);
            instance.Mask.SetViewModel(sceneModel.MaskModel);
            instance.Transform.SetViewModel(sceneModel.TransformModel);

            instance.Components.Clear();
            foreach (EntityModel entityModel in sceneModel.ComponentModels)
            {
                Entity entity = ComponentFactory.CreateEntity(instance);
                entity.SetViewModel(entityModel);
            }
        }


        private static EntityModel GetModel(this Entity instance)
        {
            EntityModel entityModel = new EntityModel();

            entityModel.Enabled = instance.Enabled;
            entityModel.Name = instance.Name;
            entityModel.ID = instance.ID;

            entityModel.BeatModifierModel = instance.BeatModifier.GetModel();
            entityModel.TextureModel = instance.Texture.GetModel();
            entityModel.GeometryModel = instance.Geometry.GetModel();
            entityModel.ColorationModel = instance.Coloration.GetModel();

            return entityModel;
        }

        private static void SetViewModel(this Entity instance, IComponentModel componentModel)
        {
            var entityModel = componentModel as EntityModel;

            instance.Enabled = entityModel.Enabled;
            instance.Name = entityModel.Name;
            instance.ID = entityModel.ID;

            instance.BeatModifier.SetViewModel(entityModel.BeatModifierModel);
            instance.Texture.SetViewModel(entityModel.TextureModel);
            instance.Geometry.SetViewModel(entityModel.GeometryModel);
            instance.Coloration.SetViewModel(entityModel.ColorationModel);
        }
    }
}