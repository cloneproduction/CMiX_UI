using Memento;
using CMiX.MVVM;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Layer : ViewModel, ICopyPasteModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Layer(MasterBeat masterBeat, string messageAddress,  Sender sender, Mementor mementor) 
        {
            Enabled = false;
            MessageAddress =  messageAddress;
            Sender = sender;
            Mementor = mementor;

            LayerName = messageAddress;           

            BlendMode = new BlendMode(masterBeat, MessageAddress, sender, mementor);
            Fade = new Slider(MessageAddress + nameof(Fade), sender, mementor);
            Content = new Content(masterBeat, MessageAddress, sender, mementor);
            Mask = new Mask(masterBeat, MessageAddress, sender, mementor);
            PostFX = new PostFX(MessageAddress, sender, mementor);
        }
        #endregion

        #region PROPERTIES

        private string _layername;
        public string LayerName
        {
            get => _layername;
            set => SetAndNotify(ref _layername, value);
        }

        private string _displayName;
        public string DisplayName
        {
            get => _displayName;
            set => SetAndNotify(ref _displayName, value);
        }

        private int _id;
        public int ID
        {
            get => _id;
            set => SetAndNotify(ref _id, value);
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
                //SendMessages(MessageAddress + nameof(Out), Out);
            }
        }

        public string MessageAddress { get; set; }
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }

        public Slider Fade { get; }
        public Content Content { get; }
        public Mask Mask { get; }
        public PostFX PostFX { get; }
        public BlendMode BlendMode { get; }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            BlendMode.UpdateMessageAddress($"{MessageAddress}{nameof(BlendMode)}/");
            Fade.UpdateMessageAddress($"{MessageAddress}{nameof(Fade)}/");
            Content.UpdateMessageAddress($"{MessageAddress}{nameof(Content)}/");
            Mask.UpdateMessageAddress($"{MessageAddress}{nameof(Mask)}/");
            PostFX.UpdateMessageAddress($"{MessageAddress}{nameof(PostFX)}/");
        }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyModel(IModel model)
        {
            LayerModel layerModel = model as LayerModel;
            layerModel.MessageAddress = MessageAddress;
            layerModel.LayerName = LayerName;
            layerModel.DisplayName = DisplayName;
            layerModel.ID = ID;

            Fade.CopyModel(layerModel.Fade);
            BlendMode.CopyModel(layerModel.BlendMode);
            Content.CopyModel(layerModel.ContentModel);
            Mask.CopyModel(layerModel.MaskModel);
            PostFX.CopyModel(layerModel.PostFXModel);
        }

        public void PasteModel(IModel model)
        {
            Sender.Disable();

            LayerModel layerModel = model as LayerModel;
            MessageAddress = layerModel.MessageAddress;
            LayerName = layerModel.LayerName;
            DisplayName = layerModel.DisplayName;
            Out = layerModel.Out;
            ID = layerModel.ID;

            Fade.PasteModel(layerModel.Fade);
            BlendMode.PasteModel(layerModel.BlendMode);
            Content.PasteModel(layerModel.ContentModel);
            Mask.PasteModel(layerModel.MaskModel);
            PostFX.PasteModel(layerModel.PostFXModel);

            Sender.Enable();
        }

        public void Reset()
        {
            Enabled = true;

            BlendMode.Reset();
            Fade.Reset();
            Content.Reset();
            Mask.Reset();
            PostFX.Reset();
        }
        #endregion
    }
}