using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components
{
    public class Layer : Component
    {
        public Layer(Composition composition, LayerModel layerModel) 
            : base (layerModel)
        {
            Fade = new Slider(nameof(Fade), layerModel.Fade);

            MasterBeat = composition.MasterBeat;
            //Visibility = new Visibility(this.MessageDispatcher, composition.Visibility, layerModel.VisibilityModel);

            PostFX = new PostFX(layerModel.PostFXModel);
            BlendMode = new BlendMode(layerModel.BlendMode);
            Mask = new Mask(layerModel.MaskModel);

            ComponentFactory = new SceneFactory(this);
        }

        public override void SetReceiver(IMessageReceiver<Component> messageReceiver)
        {
            Fade.SetReceiver(MessageReceiver);
            PostFX.SetReceiver(MessageReceiver);
            BlendMode.SetReceiver(MessageReceiver);
            Mask.SetReceiver(MessageReceiver);
        }

        public override void SetSender(ComponentSender messageSender)
        {
            Fade.SetSender(MessageSender);
            PostFX.SetSender(MessageSender);
            BlendMode.SetSender(MessageSender);
            Mask.SetSender(MessageSender);
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


        public override IModel GetModel()
        {
            LayerModel model = new LayerModel(this.ID);

            model.Enabled = this.Enabled;
            model.Name = this.Name;
            model.Out = this.Out;

            model.Fade = (SliderModel)this.Fade.GetModel();
            model.BlendMode = (BlendModeModel)this.BlendMode.GetModel();

            foreach (Component item in this.Components)
                model.ComponentModels.Add(item.GetModel() as IComponentModel);

            return model;
        }


        public override void SetViewModel(IModel model)
        {
            LayerModel layerModel = model as LayerModel;

            this.Out = layerModel.Out;
            this.Fade.SetViewModel(layerModel.Fade);
            this.BlendMode.SetViewModel(layerModel.BlendMode);

            this.Components.Clear();
            foreach (var componentModel in layerModel.ComponentModels)
            {
                var newComponent = this.ComponentFactory.CreateComponent(componentModel);
                //newComponent.SetReceiver(MessageReceiver);
                //newComponent.SetSender(MessageSender);
                this.AddComponent(newComponent);
            }
        }
    }
}