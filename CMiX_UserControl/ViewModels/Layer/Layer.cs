using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Layer : Component
    {
        #region CONSTRUCTORS
        public Layer(int id, Beat beat, string messageAddress, MessageService messageService, Mementor mementor)
            : base(id, beat, messageAddress, messageService, mementor)
        {
            Scene scene = new Scene(0, beat, messageAddress, messageService, mementor);
            Components.Add(scene);

            Mask mask = new Mask(0, beat, messageAddress, messageService, mementor);
            Components.Add(mask);

            PostFX = new PostFX(messageAddress, messageService, mementor);
            BlendMode = new BlendMode(beat, messageAddress, messageService, mementor);
            Fade = new Slider(messageAddress + nameof(Fade), messageService, mementor);
        }
        #endregion

        #region PROPERTIES
        private bool _out;
        public bool Out
        {
            get => _out;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(Out));
                SetAndNotify(ref _out, value);
                //if (Out)
                    //Sender.SendMessages(MessageAddress + nameof(Out), Out);
            }
        }

        public Slider Fade { get; set; }
        public Mask Mask { get; set; }
        public PostFX PostFX { get; set; }
        public BlendMode BlendMode { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public override IComponentModel GetModel()
        {
            LayerModel layerModel = new LayerModel();

            layerModel.Name = Name;
            layerModel.ID = ID;
            layerModel.Out = Out;

            layerModel.Fade = Fade.GetModel();
            layerModel.BlendMode = BlendMode.GetModel();
            layerModel.PostFXModel = PostFX.GetModel();

            foreach (var item in Components)
                layerModel.ComponentModels.Add(item.GetModel());

            return layerModel;
        }

        public override void SetViewModel(IComponentModel model)
        {
            MessageService.Disable();

            var layerModel = model as LayerModel;

            Name = layerModel.Name;
            Out = layerModel.Out;
            ID = layerModel.ID;

            Fade.SetViewModel(layerModel.Fade);
            BlendMode.SetViewModel(layerModel.BlendMode);
            PostFX.SetViewModel(layerModel.PostFXModel);

            Components.Clear();
            foreach (var componentModel in layerModel.ComponentModels)
            {
                Component component = null;

                if(componentModel is SceneModel)
                    component = new Scene(0, this.Beat, this.MessageAddress, this.MessageService, this.Mementor);

                else if (componentModel is MaskModel)
                    component = new Mask(0, this.Beat, this.MessageAddress, this.MessageService, this.Mementor);

                if(component != null)
                {
                    component.SetViewModel(componentModel);
                    AddComponent(component);
                }
            }

            MessageService.Enable();
        }

        public void Reset()
        {
            Enabled = true;

            BlendMode.Reset();
            Fade.Reset();
            //Mask.Reset();
            PostFX.Reset();
        }
        #endregion
    }
}