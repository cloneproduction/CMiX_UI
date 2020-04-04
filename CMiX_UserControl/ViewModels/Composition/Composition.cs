using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public class Composition : Component, ISendable, IUndoable, IGetSet<CompositionModel>
    {
        #region CONSTRUCTORS
        public Composition(int id, string messageAddress, Beat masterBeat, MessageService messageService, Assets assets, Mementor mementor)
        {
            ID = id;
            Name = "Composition " + id.ToString();

            MessageAddress = $"{messageAddress}{nameof(Composition)}/";
            MessageService = messageService;
            Beat = masterBeat; 

            MessageValidationManager = new MessageValidationManager(MessageService);
            Assets = assets;
            Mementor = mementor;

            Transition = new Slider("/Transition", MessageService, Mementor);
            Camera = new Camera(MessageService, MessageAddress, Beat, Mementor);

            Components = new ObservableCollection<Component>();
        }
        #endregion

        #region PROPERTIES
        public MessageValidationManager MessageValidationManager { get; set; }
        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        #endregion


        #region COPY/PASTE COMPOSITIONS
        public CompositionModel GetModel()
        {
            CompositionModel compositionModel = new CompositionModel();
            compositionModel.Name = Name;
            compositionModel.IsVisible = IsVisible;
            compositionModel.ID = ID;

            compositionModel.CameraModel = Camera.GetModel();
            compositionModel.MasterBeatModel = Beat.GetModel();
            compositionModel.TransitionModel = Transition.GetModel();

            foreach (IGetSet<LayerModel> item in Components)
            {
                compositionModel.ComponentModels.Add(item.GetModel());
            }

            return compositionModel;
        }

        public void SetViewModel(CompositionModel model)
        {
            //MessageService.Disable();

            Name = model.Name;
            IsVisible = model.IsVisible;
            ID = model.ID;

            Beat.SetViewModel(model.MasterBeatModel);
            Camera.SetViewModel(model.CameraModel);
            Transition.SetViewModel(model.TransitionModel);

            Components.Clear();
            foreach (LayerModel componentModel in model.ComponentModels)
            {
                Layer layer = new Layer(0, this.Beat, this.MessageAddress, this.MessageService, this.Assets, this.Mementor);
                layer.SetViewModel(componentModel);
                this.AddComponent(layer);
            }
            //MessageService.Enable();
        }
        #endregion
    }
}