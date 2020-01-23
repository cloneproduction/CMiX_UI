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

            Layers = new ObservableCollection<Layer>();
            //Entities = new ObservableCollection<Entity>();

            EntityEditor = new EntityEditor(messageService, MasterBeat, Assets, Mementor);
            LayerEditor = new LayerEditor(Layers, messageService, MessageAddress, MasterBeat, assets, mementor);
        }
        #endregion

        #region PROPERTIES
        public ICommand AddEntityCommand { get; set; }
        public ICommand DeleteEntityCommand { get; set; }

        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        public Assets Assets { get; set; }

        

        public MessageValidationManager MessageValidationManager { get; set; }
        public MasterBeat MasterBeat { get; set; }
        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        public LayerEditor LayerEditor { get; set; }
        public ObservableCollection<Layer> Layers { get; set; }

        public ObservableCollection<IEditable> EditableObject { get; set; }

        public EntityEditor EntityEditor { get; set; }
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

        private Layer _selectedLayer;
        public Layer SelectedLayer
        {
            get => _selectedLayer;
            set => SetAndNotify(ref _selectedLayer, value);
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
            compositionModel.LayerEditorModel = LayerEditor.GetModel();
            return compositionModel;
        }

        public void SetViewModel(CompositionModel compositionModel)
        {
            MessageService.Disable();

            Name = compositionModel.Name;
            MasterBeat.SetViewModel(compositionModel.MasterBeatModel);
            Camera.SetViewModel(compositionModel.CameraModel);
            Transition.SetViewModel(compositionModel.TransitionModel);
            LayerEditor.SetViewModel(compositionModel.LayerEditorModel);

            MessageService.Enable();
        }
        #endregion
    }
}