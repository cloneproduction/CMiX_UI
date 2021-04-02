using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.Components.Factories;

namespace CMiX.MVVM.ViewModels.Components
{
    public class Scene : Component
    {
        public Scene(Layer layer, SceneModel sceneModel) 
            : base (sceneModel)
        {
            MasterBeat = layer.MasterBeat;

            Visibility = new Visibility(layer.MessageDispatcher, layer.Visibility, sceneModel.VisibilityModel);
            BeatModifier = new BeatModifier(this.MessageDispatcher, layer.MasterBeat, sceneModel.BeatModifierModel);
            PostFX = new PostFX(this.MessageDispatcher, sceneModel.PostFXModel);
            Mask = new Mask(this.MessageDispatcher, sceneModel.MaskModel);
            Transform = new Transform(this.MessageDispatcher, sceneModel.TransformModel);

            ComponentFactory = new EntityFactory(this);
        }


        public Transform Transform { get; set; }
        public Mask Mask { get; set; }
        public PostFX PostFX { get; set; }
        public BeatModifier BeatModifier { get; set; }
        public MasterBeat MasterBeat { get; set; }


        public override IModel GetModel()
        {
            SceneModel model = new SceneModel(this.ID);

            model.Enabled = this.Enabled;
            model.Name = this.Name;

            model.BeatModifierModel = (BeatModifierModel)this.BeatModifier.GetModel();
            model.PostFXModel = (PostFXModel)this.PostFX.GetModel();
            model.MaskModel = (MaskModel)this.Mask.GetModel();
            model.TransformModel = (TransformModel)this.Transform.GetModel();

            foreach (Component item in this.Components)
                model.ComponentModels.Add(item.GetModel() as IComponentModel);

            return model;
        }

        public override void SetViewModel(IModel model)
        {
            SceneModel sceneModel = model as SceneModel;

            this.BeatModifier.SetViewModel(sceneModel.BeatModifierModel);
            this.PostFX.SetViewModel(sceneModel.PostFXModel);
            this.Mask.SetViewModel(sceneModel.MaskModel);
            this.Transform.SetViewModel(sceneModel.TransformModel);

            this.Components.Clear();
            foreach (var componentModel in sceneModel.ComponentModels)
                this.ComponentFactory.CreateComponent(sceneModel);
        }
    }
}