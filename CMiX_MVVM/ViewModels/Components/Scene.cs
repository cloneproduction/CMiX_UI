using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components
{
    public class Scene : Component
    {
        public Scene(MessageDispatcher messageDispatcher, Layer layer, SceneModel sceneModel) : base (messageDispatcher, sceneModel)
        {

            MasterBeat = layer.MasterBeat;
            Visibility = new Visibility(layer, layer.Visibility);

            BeatModifier = new BeatModifier(this, layer.MasterBeat, sceneModel.BeatModifierModel);
            PostFX = new PostFX(this, sceneModel.PostFXModel);
            Mask = new Mask(this, sceneModel.MaskModel);
            Transform = new Transform(this, sceneModel.TransformModel);

            ComponentFactory = new EntityFactory();
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

            
            GetComponents(this, model);

            return model;
        }

        public override void SetViewModel(IModel model)
        {
            SceneModel sceneModel = model as SceneModel;

            this.BeatModifier.SetViewModel(sceneModel.BeatModifierModel);
            this.PostFX.SetViewModel(sceneModel.PostFXModel);
            this.Mask.SetViewModel(sceneModel.MaskModel);
            this.Transform.SetViewModel(sceneModel.TransformModel);

            SetComponents(this, sceneModel);
        }
    }
}