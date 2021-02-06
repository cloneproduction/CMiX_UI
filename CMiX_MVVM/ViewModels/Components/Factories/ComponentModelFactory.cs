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


        public static void SetViewModel(this Component instance, IComponentModel model)
        {
            instance.Name = model.Name;
            instance.ID = model.ID;
            instance.IsVisible = model.IsVisible;
            instance.Enabled = model.Enabled;

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

            instance.Components.Clear();
            foreach (CompositionModel compositionModel in projectModel.ComponentModels)
            {
                instance.CreateAndAddComponent().SetViewModel(compositionModel);
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

            instance.MasterBeat.SetViewModel(compositionModel.MasterBeatModel);
            instance.Camera.SetViewModel(compositionModel.CameraModel);
            instance.Transition.SetViewModel(compositionModel.TransitionModel);

            SetComponents(instance, compositionModel);
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

            GetComponents(instance, model);

            return model;
        }



        private static void SetViewModel(this Layer instance, IComponentModel componentModel)
        {
            var layerModel = componentModel as LayerModel;

            instance.Out = layerModel.Out;
            instance.Fade.SetViewModel(layerModel.Fade);
            instance.BlendMode.SetViewModel(layerModel.BlendMode);

            SetComponents(instance, layerModel); 
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

            instance.BeatModifier.SetViewModel(sceneModel.BeatModifierModel);
            instance.PostFX.SetViewModel(sceneModel.PostFXModel);
            instance.Mask.SetViewModel(sceneModel.MaskModel);
            instance.Transform.SetViewModel(sceneModel.TransformModel);

            SetComponents(instance, sceneModel);
        }


        private static EntityModel GetModel(this Entity instance)
        {
            EntityModel model = new EntityModel();

            model.Enabled = instance.Enabled;
            model.Name = instance.Name;
            model.ID = instance.ID;

            model.BeatModifierModel = instance.BeatModifier.GetModel();
            model.TextureModel = instance.Texture.GetModel();
            model.GeometryModel = instance.Geometry.GetModel();
            model.ColorationModel = instance.Coloration.GetModel();

            return model;
        }

        private static void SetViewModel(this Entity instance, IComponentModel componentModel)
        {
            var entityModel = componentModel as EntityModel;

            instance.BeatModifier.SetViewModel(entityModel.BeatModifierModel);
            instance.Texture.SetViewModel(entityModel.TextureModel);
            instance.Geometry.SetViewModel(entityModel.GeometryModel);
            instance.Coloration.SetViewModel(entityModel.ColorationModel);
        }

        private static void SetComponents(Component component, IComponentModel componentModel)
        {
            component.Components.Clear();
            foreach (var model in componentModel.ComponentModels)
                component.CreateAndAddComponent().SetViewModel(model);
        }

        private static void GetComponents(Component component, IComponentModel componentModel)
        {
            foreach (Component item in component.Components)
                componentModel.ComponentModels.Add(item.GetModel());
        }
    }
}