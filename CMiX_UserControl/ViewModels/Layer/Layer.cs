using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;

namespace CMiX.ViewModels
{
    public class Layer : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Layer(MasterBeat masterBeat, string messageAddress,  Messenger messenger, Mementor mementor) 
        {
            MessageAddress =  messageAddress;
            Messenger = messenger;
            Mementor = mementor;

            Enabled = false;
            LayerName = messageAddress;           

            BlendMode = new BlendMode(masterBeat, MessageAddress, messenger, mementor);
            Fade = new Slider(MessageAddress + nameof(Fade), messenger, mementor);

            Content = new Content(masterBeat, MessageAddress, messenger, mementor);
            Mask = new Mask(masterBeat, MessageAddress, messenger, mementor);
            PostFX = new PostFX(MessageAddress, messenger, mementor);
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
        public Messenger Messenger { get; set; }
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
        public void Copy(LayerModel layermodel)
        {
            layermodel.MessageAddress = MessageAddress;
            layermodel.LayerName = LayerName;
            layermodel.DisplayName = DisplayName;
            layermodel.ID = ID;

            Fade.Copy(layermodel.Fade);
            BlendMode.Copy(layermodel.BlendMode);
            Content.Copy(layermodel.ContentModel);
            Mask.Copy(layermodel.MaskModel);
            PostFX.Copy(layermodel.PostFXModel);
        }

        public void Paste(LayerModel layerModel)
        {
            Messenger.Disable();

            MessageAddress = layerModel.MessageAddress;
            LayerName = layerModel.LayerName;
            DisplayName = layerModel.DisplayName;
            Out = layerModel.Out;
            ID = layerModel.ID;

            Fade.Paste(layerModel.Fade);
            BlendMode.Paste(layerModel.BlendMode);
            Content.Paste(layerModel.ContentModel);
            Mask.Paste(layerModel.MaskModel);
            PostFX.Paste(layerModel.PostFXModel);

            Messenger.Enable();
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