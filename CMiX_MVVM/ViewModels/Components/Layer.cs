using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Layer : Component
    {
        public Layer(int id, MessageTerminal MessageTerminal, Composition composition) : base (id, MessageTerminal)
        {
            MasterBeat = composition.MasterBeat;
            this.ParentIsVisible = composition.ParentIsVisible;
            PostFX = new PostFX(nameof(PostFX), this);
            BlendMode = new BlendMode(nameof(BlendMode), this);
            Fade = new Slider(nameof(Fade), this);
            Mask = new Mask(nameof(Mask), this);
            ComponentFactory = new SceneFactory(MessageTerminal);
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

        public override void SetViewModel(IModel model)
        {
            LayerModel layerModel = model as LayerModel;

            this.Out = layerModel.Out;
            this.Fade.SetViewModel(layerModel.Fade);
            this.BlendMode.SetViewModel(layerModel.BlendMode);

            SetComponents(this, layerModel);
        }

        public override IModel GetModel()
        {
            LayerModel model = new LayerModel();

            model.Enabled = this.Enabled;
            model.Name = this.Name;
            model.ID = this.ID;
            model.Out = this.Out;

            model.Fade = (SliderModel)this.Fade.GetModel();
            model.BlendMode = (BlendModeModel)this.BlendMode.GetModel();

            GetComponents(this, model);

            return model;
        }
    }
}