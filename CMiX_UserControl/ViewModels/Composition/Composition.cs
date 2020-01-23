using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class Composition : ViewModel, ISendable, IUndoable, IEditable
    {
        #region CONSTRUCTORS
        public Composition(MessageService messageService, string messageAddress, Assets assets, Mementor mementor)
        {
            Name = string.Empty;

            MessageAddress = $"{messageAddress}{nameof(Composition)}/";
            MessageService = messageService;
            MessageValidationManager = new MessageValidationManager(messageService);
            Assets = assets;
            Mementor = mementor;

            ObjectEditor = new ObjectEditor();

            Transition = new Slider("/Transition", messageService, Mementor);
            MasterBeat = new MasterBeat(messageService);
            Camera = new Camera(messageService, MessageAddress, MasterBeat, Mementor);

            LayerManager = new LayerManager(MessageService);
            Layers = new ObservableCollection<Layer>();

            AddLayerCommand = new RelayCommand(p => AddLayer());
            DeleteSelectedLayerCommand = new RelayCommand(p => DeleteSelectedLayer());
            DuplicateSelectedLayerCommand = new RelayCommand(p => DuplicateSelectedLayer());
        }
        #endregion

        #region PROPERTIES
        public ICommand AddEntityCommand { get; set; }
        public ICommand DeleteEntityCommand { get; set; }

        public ICommand AddLayerCommand { get; set; }
        public ICommand DuplicateSelectedLayerCommand { get; set; }
        public ICommand DeleteSelectedLayerCommand { get; set; }

        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        public Assets Assets { get; set; }

        public MessageValidationManager MessageValidationManager { get; set; }
        public MasterBeat MasterBeat { get; set; }
        public Camera Camera { get; set; }
        public Slider Transition { get; set; }

        public ObjectEditor ObjectEditor { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private bool _isRenaming;
        public bool IsRenaming
        {
            get => _isRenaming;
            set => SetAndNotify(ref _isRenaming, value);
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
        public void RenameSelectedLayer()
        {
            if (SelectedLayer != null)
                SelectedLayer.IsRenaming = true;
        }

        public void AddLayer()
        {
            LayerManager.CreateLayer(this);
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
            compositionModel.MasterBeatModel = MasterBeat.GetModel();
            compositionModel.TransitionModel = Transition.GetModel();
            //compositionModel.LayerEditorModel = LayerEditor.GetModel();
            return compositionModel;
        }

        public void SetViewModel(CompositionModel compositionModel)
        {
            MessageService.Disable();

            Name = compositionModel.Name;
            MasterBeat.SetViewModel(compositionModel.MasterBeatModel);
            Camera.SetViewModel(compositionModel.CameraModel);
            Transition.SetViewModel(compositionModel.TransitionModel);
            //LayerEditor.SetViewModel(compositionModel.LayerEditorModel);

            MessageService.Enable();
        }
        #endregion
    }
}