using System.Windows.Input;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using System.Collections.ObjectModel;
using System.Windows;
using System;

namespace CMiX.Studio.ViewModels
{
    public class Composition : ViewModel, ISendable, IUndoable, IComponent, IGetSet<CompositionModel>
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

            Components = new ObservableCollection<IComponent>();

            AddComponentCommand = new RelayCommand(p => AddComponent(p as IComponent));
            RemoveComponentCommand = new RelayCommand(p => RemoveComponent(p as IComponent));
            RenameCommand = new RelayCommand(p => Rename());
        }
        #endregion

        #region PROPERTIES
        public ICommand RenameCommand { get;  }
        public ICommand AddComponentCommand { get; set; }
        public ICommand RemoveComponentCommand { get; }

        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        public Assets Assets { get; set; }

        public MessageValidationManager MessageValidationManager { get; set; }
        public Beat Beat { get; set; }
        public Camera Camera { get; set; }
        public Slider Transition { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private bool _isVisible = false;
        public bool IsVisible
        {
            get
            {
                if (!ParentIsVisible)
                    return true;
                else
                    return _isVisible;
            }
            set
            {
                if(ParentIsVisible)
                    SetAndNotify(ref _isVisible, value);
                SetVisibility();
            }
        }

        private bool _parentIsVisible;
        public bool ParentIsVisible
        {
            get => _parentIsVisible;
            set
            {
                SetAndNotify(ref _parentIsVisible, value);
                Notify(nameof(IsVisible));
            }
        }

        public void SetVisibility()
        {
            foreach (var item in this.Components)
                item.ParentIsVisible = IsVisible;
        }


        private bool _isRenaming;
        public bool IsRenaming
        {
            get => _isRenaming;
            set => SetAndNotify(ref _isRenaming, value);
        }

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

        private int _id;
        public int ID
        {
            get => _id;
            set => SetAndNotify(ref _id, value);
        }

        public ObservableCollection<IComponent> Components { get; set; }

        #endregion

        #region ADD/DUPLICATE/DELETE LAYERS
        public void Rename()
        {
            IsRenaming = true;
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