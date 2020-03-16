using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.Resources;
using System.Collections.Specialized;
using System.Windows.Input;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;

namespace CMiX.Studio.ViewModels
{
    public class Layer : ViewModel, ISendable, IUndoable, IComponent, IGetSet<LayerModel>
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

            Components = new ObservableCollection<IComponent>();
            Scene scene = new Scene(this.Beat, this.MessageAddress, this.MessageService, this.Assets, this.Mementor);
            Mask mask = new Mask(this.Beat, this.MessageAddress, this.MessageService, this.Assets, this.Mementor);
            Components.Add(scene);
            Components.Add(mask);

            PostFX = new PostFX(MessageAddress, messageService, mementor);
            BlendMode = new BlendMode(beat, MessageAddress, messageService, mementor);
            Fade = new Slider(MessageAddress + nameof(Fade), messageService, mementor);

            RenameCommand = new RelayCommand(p => Rename());
            RemoveComponentCommand = new RelayCommand(p => RemoveComponent(p as IComponent));
        }
        #endregion

        public ICommand RenameCommand { get;  }
        public ICommand RemoveComponentCommand { get; }

        public ObservableCollection<IComponent> Components { get; set; }

        private Entity _selectedEntity;
        public Entity SelectedEntity
        {
            get => _selectedEntity;
            set => SetAndNotify(ref _selectedEntity, value);
        }

        #region PROPERTIES
        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetAndNotify(ref _isVisible, value);
        }

        //private bool _isMask;
        //public bool IsMask
        //{
        //    get => _isMask;
        //    set => SetAndNotify(ref _isMask, value);
        //}

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndNotify(ref _isSelected, value);
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetAndNotify(ref _isExpanded, value);
        }

        private bool _isRenaming;
        public bool IsRenaming
        {
            get => _isRenaming;
            set => SetAndNotify(ref _isRenaming, value);
        }

        private int _id;
        public int ID
        {
            get => _id;
            set => SetAndNotify(ref _id, value);
        }

        private Beat _beat;
        public Beat Beat
        {
            get => _beat;
            set => SetAndNotify(ref _beat, value);
        }

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

        private IComponent _selectedComponent;
        public IComponent SelectedComponent
        {
            get => _selectedComponent;
            set => SetAndNotify(ref _selectedComponent, value);
        }


        public MessageService MessageService { get; set; }
        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public Assets Assets { get; set; }

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

        public void AddComponent(IComponent component)
        {
            Components.Add(component);
            IsExpanded = true;
        }

        public void RemoveComponent(IComponent component)
        {
            Components.Remove(component);
        }

        public void Rename()
        {
            IsRenaming = true;
        }
        #endregion
    }
}