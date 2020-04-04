using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public class Layer : Component, ISendable, IUndoable, IGetSet<LayerModel>
    {
        #region CONSTRUCTORS
        public Layer(int id, Beat beat, string messageAddress, MessageService messageService, Assets assets, Mementor mementor) 
        {
            ID = id;
            Name = "Layer " + id;

            Enabled = false;
            MessageAddress =  $"{messageAddress}{nameof(Layer)}/{id}/";
            MessageService = messageService;
            Mementor = mementor;
            Assets = assets;
            Beat = beat;
            ID = id;

            Components = new ObservableCollection<Component>();
            Scene scene = new Scene(this.Beat, this.MessageAddress, this.MessageService, this.Assets, this.Mementor);
            Mask mask = new Mask(this.Beat, this.MessageAddress, this.MessageService, this.Assets, this.Mementor);
            Components.Add(scene);
            Components.Add(mask);

            PostFX = new PostFX(MessageAddress, messageService, mementor);
            BlendMode = new BlendMode(beat, MessageAddress, messageService, mementor);
            Fade = new Slider(MessageAddress + nameof(Fade), messageService, mementor);
        }
        #endregion
        private Entity _selectedEntity;
        public Entity SelectedEntity
        {
            get => _selectedEntity;
            set => SetAndNotify(ref _selectedEntity, value);
        }

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
        public LayerModel GetModel()
        {
            LayerModel layerModel = new LayerModel();

            layerModel.Name = Name;
            layerModel.ID = ID;
            layerModel.Out = Out;

            layerModel.Fade = Fade.GetModel();
            layerModel.BlendMode = BlendMode.GetModel();
            layerModel.PostFXModel = PostFX.GetModel();

            foreach (var item in Components)
            {
                if (item is IGetSet<SceneModel>)
                    layerModel.ComponentModels.Add(((IGetSet<SceneModel>)item).GetModel());

                if (item is IGetSet<MaskModel>)
                    layerModel.ComponentModels.Add(((IGetSet<MaskModel>)item).GetModel());
            }

            return layerModel;
        }

        public void SetViewModel(LayerModel model)
        {
            MessageService.Disable();

            Name = model.Name;
            Out = model.Out;
            ID = model.ID;

            Fade.SetViewModel(model.Fade);
            BlendMode.SetViewModel(model.BlendMode);
            PostFX.SetViewModel(model.PostFXModel);

            Components.Clear();
            foreach (IComponentModel componentModel in model.ComponentModels)
            {
                if(componentModel is SceneModel)
                {
                    Scene scene = new Scene(this.Beat, this.MessageAddress, this.MessageService, this.Assets, this.Mementor);
                    scene.SetViewModel(componentModel as SceneModel);
                    this.AddComponent(scene);
                }

                if (componentModel is MaskModel)
                {
                    Mask mask = new Mask(this.Beat, this.MessageAddress, this.MessageService, this.Assets, this.Mementor);
                    mask.SetViewModel(componentModel as MaskModel);
                    this.AddComponent(mask);
                }
            }

            MessageService.Enable();
        }

        public void Reset()
        {
            Enabled = true;

            BlendMode.Reset();
            Fade.Reset();
            Mask.Reset();
            PostFX.Reset();
        }
        #endregion
    }
}