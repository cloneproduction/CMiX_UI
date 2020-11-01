using CMiX.MVVM.Interfaces;
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


        public static IComponentModel CastObject(this Component instance, object obj)
        {
            if (obj is IComponentModel)
                return obj as IComponentModel;
            else
                return null;
        }

        public static void SetViewModel(this Component instance, object obj)
        {
            var model = obj as IComponentModel;
            if (model is IComponentModel)
            {
                if (instance is Project)
                    ((Project)instance).SetViewModel(model);

                else if (instance is Composition)
                    ((Composition)instance).SetViewModel(model);

                else if (instance is Layer)
                    ((Layer)instance).SetViewModel(model);

                else if (instance is Scene)
                    ((Scene)instance).SetViewModel(model);

                else if (instance is Entity)
                    ((Entity)instance).SetViewModel(model);
            }
        }


        private static ProjectModel GetModel(this Project instance)
        {
            ProjectModel model = new ProjectModel();

            model.Enabled = instance.Enabled;
            model.ID = instance.ID;
            model.Name = instance.Name;
            model.IsVisible = instance.IsVisible;

            GetComponents(instance, model);

            foreach (IAssets asset in instance.Assets)
                model.AssetModels.Add(asset.GetModel());

            return model;
        }

        private static void SetViewModel(this Project instance, IComponentModel componentModel)
        {
            var projectModel = componentModel as ProjectModel;

            instance.ID = projectModel.ID;
            instance.Name = projectModel.Name;
            instance.IsVisible = projectModel.IsVisible;

            instance.Components.Clear();
            foreach (CompositionModel compositionModel in projectModel.ComponentModels)
            {
                Composition composition = ComponentFactory.CreateComponent(instance) as Composition;
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
            CompositionModel model = new CompositionModel();

            model.Name = instance.Name;
            model.IsVisible = instance.IsVisible;
            model.ID = instance.ID;
            model.MasterBeatModel = instance.MasterBeat.GetModel();
            model.CameraModel = instance.Camera.GetModel();
            model.TransitionModel = instance.Transition.GetModel();

            GetComponents(instance, model);

            return model;
        }


        private static void SetViewModel(this Composition instance, IComponentModel model)
        {
            var compositionModel = model as CompositionModel;

            instance.Enabled = compositionModel.Enabled;
            instance.Name = compositionModel.Name;
            instance.ID = compositionModel.ID;
            instance.IsVisible = compositionModel.IsVisible;

            instance.MasterBeat.SetViewModel(compositionModel.MasterBeatModel);
            instance.Camera.SetViewModel(compositionModel.CameraModel);
            instance.Transition.SetViewModel(compositionModel.TransitionModel);

            instance.Components.Clear();
            foreach (LayerModel componentModel in model.ComponentModels)
            {
                Layer layer = ComponentFactory.CreateComponent(instance) as Layer;
                layer.SetViewModel(componentModel);
            }
        }


        private static LayerModel GetModel(this Layer instance)
        {
            LayerModel model = new LayerModel();

            model.Enabled = instance.Enabled;
            model.Name = instance.Name;
            model.ID = instance.ID;
            model.Out = instance.Out;

            model.Fade = instance.Fade.GetModel();
            model.BlendMode = instance.BlendMode.GetModel();
            model.PostFXModel = instance.PostFX.GetModel();

            GetComponents(instance, model);

            return model;
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
                Scene scene = ComponentFactory.CreateComponent(instance) as Scene;
                scene.SetViewModel(componentModel);
            }
        }


        private static SceneModel GetModel(this Scene instance)
        {
            SceneModel model = new SceneModel();

            model.Enabled = instance.Enabled;
            model.ID = instance.ID;
            model.Name = instance.Name;

            model.BeatModifierModel = instance.BeatModifier.GetModel();
            model.PostFXModel = instance.PostFX.GetModel();
            model.MaskModel = instance.Mask.GetModel();
            model.TransformModel = instance.Transform.GetModel();

            GetComponents(instance, model);

            return model;
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
                Entity entity = ComponentFactory.CreateComponent(instance) as Entity;
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

        private static void GetComponents(Component component, IComponentModel componentModel)
        {
            foreach (var item in component.Components)
                componentModel.ComponentModels.Add(item.GetModel());
        }
    }
}