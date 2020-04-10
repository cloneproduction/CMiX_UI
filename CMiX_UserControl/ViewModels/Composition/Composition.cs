using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public class Composition : Component
    {
        #region CONSTRUCTORS
        public Composition(int id, string messageAddress, Beat beat, MessageService messageService, Mementor mementor) 
            : base (id, beat, messageAddress, messageService, mementor)
        {
            MessageValidationManager = new MessageValidationManager(MessageService);

            Transition = new Slider("/Transition", MessageService, Mementor);
            Camera = new Camera(MessageService, MessageAddress, Beat, Mementor);
        }
        #endregion

        #region PROPERTIES
        public MessageValidationManager MessageValidationManager { get; set; }
        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        #endregion


        #region COPY/PASTE COMPOSITIONS
        public override IComponentModel GetModel()
        {
            CompositionModel compositionModel = new CompositionModel();
            compositionModel.Name = Name;
            compositionModel.IsVisible = IsVisible;
            compositionModel.ID = ID;

            compositionModel.CameraModel = Camera.GetModel();
            compositionModel.MasterBeatModel = Beat.GetModel();
            compositionModel.TransitionModel = Transition.GetModel();

            foreach (Component component in Components)
                compositionModel.ComponentModels.Add(component.GetModel());

            return compositionModel;
        }

        public override void SetViewModel(IComponentModel model)
        {
            //MessageService.Disable();
            var compositionModel = model as CompositionModel;

            Name = compositionModel.Name;
            IsVisible = compositionModel.IsVisible;
            ID = compositionModel.ID;

            Beat.SetViewModel(compositionModel.MasterBeatModel);
            Camera.SetViewModel(compositionModel.CameraModel);
            Transition.SetViewModel(compositionModel.TransitionModel);

            Components.Clear();
            foreach (LayerModel componentModel in model.ComponentModels)
            {
                Layer layer = new Layer(0, this.Beat, this.MessageAddress, this.MessageService, this.Mementor);
                layer.SetViewModel(componentModel);
                this.AddComponent(layer);
            }
            //MessageService.Enable();
        }
        #endregion
    }
}