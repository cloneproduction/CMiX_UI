using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    public class Layer : SendableViewModel
    {
        #region CONSTRUCTORS
        public Layer(MasterBeat masterBeat, string messageAddress,  ObservableCollection<ServerValidation> serverValidations, Mementor mementor) 
            : base (serverValidations, mementor)
        {
            MessageAddress =  messageAddress;

            LayerName = messageAddress;           
            Enabled = false;

            BlendMode = new BlendMode(masterBeat, MessageAddress, serverValidations, mementor);
            Fade = new Slider(MessageAddress + nameof(Fade), serverValidations, mementor);
            Content = new Content(masterBeat, MessageAddress, serverValidations, mementor);
            Mask = new Mask(masterBeat, MessageAddress, serverValidations, mementor);
            Coloration = new Coloration(MessageAddress, serverValidations, mementor, masterBeat);
            PostFX = new PostFX(MessageAddress, serverValidations, mementor);
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

        private bool _enabled;
        public bool Enabled
        {
            get => _enabled;
            set => SetAndNotify(ref _enabled, value);
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

        public Slider Fade { get; }
        public Content Content { get; }
        public Mask Mask { get; }
        public Coloration Coloration { get; }
        public PostFX PostFX { get; }
        public BlendMode BlendMode { get; }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            BlendMode.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(BlendMode)));
            Fade.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Fade)));
            Content.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Content)));
            Mask.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Mask)));
            Coloration.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Coloration)));
            PostFX.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(PostFX)));
        }
        #endregion



        #region COPY/PASTE/LOAD
        public void Reset()
        {
            Enabled = true;

            BlendMode.Reset();
            Fade.Reset();
            Content.Reset();
            Mask.Reset();
            Coloration.Reset();
            PostFX.Reset();
        }

        public void Copy(LayerModel layermodel)
        {
            layermodel.MessageAddress = MessageAddress;
            layermodel.LayerName = LayerName;
            layermodel.DisplayName = DisplayName;
            layermodel.ID = ID;

            BlendMode.Copy(layermodel.BlendMode);
            Fade.Copy(layermodel.Fade);
            Content.Copy(layermodel.ContentModel);
            Mask.Copy(layermodel.MaskModel);
            Coloration.Copy(layermodel.ColorationModel);
            PostFX.Copy(layermodel.PostFXModel);
        }

        public void Paste(LayerModel layerModel)
        {
            DisabledMessages();

            MessageAddress = layerModel.MessageAddress;
            LayerName = layerModel.LayerName;
            DisplayName = layerModel.DisplayName;
            Out = layerModel.Out;
            ID = layerModel.ID;

            BlendMode.Paste(layerModel.BlendMode);
            Fade.Paste(layerModel.Fade);
            Content.Paste(layerModel.ContentModel);
            Mask.Paste(layerModel.MaskModel);
            Coloration.Paste(layerModel.ColorationModel);
            PostFX.Paste(layerModel.PostFXModel);

            EnabledMessages();
        }
        #endregion
    }
}