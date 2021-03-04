using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components
{
    public class Scene : Component
    {
        public Scene(int id, MessageTerminal MessageTerminal, Layer layer) : base (id, MessageTerminal)
        {
            MasterBeat = layer.MasterBeat;
            Visibility = new Visibility(nameof(Visibility), layer, layer.Visibility);

            BeatModifier = new BeatModifier(nameof(BeatModifier), this, layer.MasterBeat);
            PostFX = new PostFX(nameof(PostFX), this);
            Mask = new Mask(nameof(Mask), this);
            Transform = new Transform(nameof(Transform), this);
            ComponentFactory = new EntityFactory(MessageTerminal);
        }

        
        public Transform Transform { get; set; }
        public Mask Mask { get; set; }
        public PostFX PostFX { get; set; }
        public BeatModifier BeatModifier { get; set; }
        public MasterBeat MasterBeat { get; set; }


        public override IModel GetModel()
        {
            SceneModel model = new SceneModel();

            model.Enabled = this.Enabled;
            model.ID = this.ID;
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