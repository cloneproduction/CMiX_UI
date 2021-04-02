using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components
{
    public class Layer : Component
    {
        public Layer(Composition composition, LayerModel layerModel, IMessageDispatcher messageDispatcher) 
            : base (layerModel, messageDispatcher)
        {
            MasterBeat = composition.MasterBeat;
            Visibility = new Visibility(this.MessageDispatcher, composition.Visibility, layerModel.VisibilityModel);

            PostFX = new PostFX(this.MessageDispatcher, layerModel.PostFXModel);
            BlendMode = new BlendMode(this.MessageDispatcher, layerModel.BlendMode);
            Fade = new Slider(nameof(Fade), this.MessageDispatcher, layerModel.Fade);
            Mask = new Mask(this.MessageDispatcher, layerModel.MaskModel);

            ComponentFactory = new SceneFactory(this, messageDispatcher);
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
                this.ComponentFactory.CreateComponent(layerModel);
            }
        }

        //public override void CreateChild()
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}