using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class Layer : ViewModel, ISendable, IUndoable, IEditable
    {
        #region CONSTRUCTORS
        public Layer(MasterBeat masterBeat, string messageAddress, int id, MessageService messageService, Assets assets, Mementor mementor) 
        {
            Enabled = false;
            MessageAddress =  $"{messageAddress}{nameof(Layer)}/{id}/";
            MessageService = messageService;
            Mementor = mementor;
            Assets = assets;
            MasterBeat = masterBeat;
            ID = id;
            Name = "Layer " + id;

            Entities = new ObservableCollection<Entity>();
            PostFX = new PostFX(MessageAddress, messageService, mementor);

            //Content = new Content(masterBeat, MessageAddress, messageService, mementor);
            Mask = new Mask(masterBeat, MessageAddress, messageService, mementor);

            BlendMode = new BlendMode(masterBeat, MessageAddress, messageService, mementor);
            Fade = new Slider(MessageAddress + nameof(Fade), messageService, mementor);
            
        }
        #endregion

        public ObservableCollection<Entity> Entities { get; set; }

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

        private bool _isMask = false;
        public bool IsMask
        {
            get => _isMask;
            set => SetAndNotify(ref _isMask, value);
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetAndNotify(ref _isVisible, value);
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndNotify(ref _isSelected, value);
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

        private Beat _masterBeat;
        public Beat MasterBeat
        {
            get => _masterBeat;
            set => SetAndNotify(ref _masterBeat, value);
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
            layerModel.IsMask = IsMask;
            layerModel.Out = Out;
            layerModel.Fade = Fade.GetModel();
            layerModel.BlendMode = BlendMode.GetModel();
            layerModel.MaskModel = Mask.GetModel();
            layerModel.PostFXModel = PostFX.GetModel();
            return layerModel;
        }

        public void SetViewModel(LayerModel layerModel)
        {
            MessageService.Disable();

            Name = layerModel.Name;
            Out = layerModel.Out;
            ID = layerModel.ID;
            IsMask = layerModel.IsMask;
            Fade.SetViewModel(layerModel.Fade);
            BlendMode.SetViewModel(layerModel.BlendMode);
            Mask.SetViewModel(layerModel.MaskModel);
            PostFX.SetViewModel(layerModel.PostFXModel);

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