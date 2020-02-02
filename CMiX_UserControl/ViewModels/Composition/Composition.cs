using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class Composition : ViewModel, ISendable, IUndoable, IComponent
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

            ComponentEditor = new ComponentEditor();

            Transition = new Slider("/Transition", MessageService, Mementor);
            
            Camera = new Camera(MessageService, MessageAddress, Beat, Mementor);

            LayerManager = new LayerManager(MessageService);
            Layers = new ObservableCollection<Layer>();

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

        public ComponentEditor ComponentEditor { get; set; }

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
            set
            {
                SetAndNotify(ref _isVisible, value);
                System.Console.WriteLine("Composition Is Visible " + IsVisible);
            }
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

        public LayerManager LayerManager { get; set; }
        public ObservableCollection<Layer> Layers { get; set; }


        private Layer _selectedLayer;
        public Layer SelectedLayer
        {
            get => _selectedLayer;
            set => SetAndNotify(ref _selectedLayer, value);
        }

        #endregion

        #region ADD/DUPLICATE/DELETE LAYERS
        public void Rename()
        {
            IsRenaming = true;
        }

        public void AddComponent(IComponent component)
        {
            Layers.Add(component as Layer);
            IsExpanded = true;
        }

        public void RemoveComponent(IComponent component)
        {
            Layers.Remove(component as Layer);
        }

        private void DuplicateSelectedLayer()
        {
            LayerManager.DuplicateLayer(this);
        }

        private void DuplicateSelectedLayerLink(object obj)
        {
            if (obj is Composition)
            {
                var composition = obj as Composition;
                LayerManager.DuplicateLayerLink(composition);
            }
        }

        private void DeleteSelectedLayer()
        {
            LayerManager.DeleteLayer(this);
        }
        #endregion

        #region COPY/PASTE COMPOSITIONS

        public CompositionModel GetModel()
        {
            CompositionModel compositionModel = new CompositionModel();
            compositionModel.Name = Name;
            compositionModel.CameraModel = Camera.GetModel();
            compositionModel.MasterBeatModel = Beat.GetModel();
            compositionModel.TransitionModel = Transition.GetModel();
            //compositionModel.LayerEditorModel = LayerEditor.GetModel();
            return compositionModel;
        }

        public void SetViewModel(IModel model)
        {
            MessageService.Disable();

            var compositionModel = model as CompositionModel;
            Name = compositionModel.Name;
            Beat.SetViewModel(compositionModel.MasterBeatModel);
            Camera.SetViewModel(compositionModel.CameraModel);
            Transition.SetViewModel(compositionModel.TransitionModel);
            //LayerEditor.SetViewModel(compositionModel.LayerEditorModel);

            MessageService.Enable();
        }
        #endregion
    }
}