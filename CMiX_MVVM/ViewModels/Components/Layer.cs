using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components
{
    public class Layer : Component
    {
        public Layer(MessageTerminal messageTerminal, Composition composition, LayerModel layerModel) 
            : base (messageTerminal, layerModel)
        {
            MasterBeat = composition.MasterBeat;
            Visibility = new Visibility(composition, composition.Visibility);

            PostFX = new PostFX(this, layerModel.PostFXModel);
            BlendMode = new BlendMode(this, layerModel.BlendMode);
            Fade = new Slider(nameof(Fade), this, layerModel.Fade);
            Mask = new Mask(this, layerModel.MaskModel);
            ComponentFactory = new SceneFactory(messageTerminal);
        }

        private bool _out;
        public bool Out
        {
            get => _out;
            set => SetAndNotify(ref _out, value);
        }

        public MasterBeat MasterBeat { get; set; }
        public Slider Fade { get; set; }
        public Mask Mask { get; set; }
        public PostFX PostFX { get; set; }
        public BlendMode BlendMode { get; set; }

        public override void Dispose()
        {
            Fade.Dispose();
            Mask.Dispose();
            PostFX.Dispose();
            BlendMode.Dispose();
            base.Dispose();
        }


        public override IModel GetModel()
        {
            LayerModel model = new LayerModel(this.ID);

            model.Enabled = this.Enabled;
            model.Name = this.Name;
            model.Out = this.Out;

            model.Fade = (SliderModel)this.Fade.GetModel();
            model.BlendMode = (BlendModeModel)this.BlendMode.GetModel();

            GetComponents(this, model);

            return model;
        }


        public override void SetViewModel(IModel model)
        {
            LayerModel layerModel = model as LayerModel;

            this.Out = layerModel.Out;
            this.Fade.SetViewModel(layerModel.Fade);
            this.BlendMode.SetViewModel(layerModel.BlendMode);

            SetComponents(this, layerModel);
        }
    }
}