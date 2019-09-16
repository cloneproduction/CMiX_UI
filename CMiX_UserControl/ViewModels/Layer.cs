using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Layer : ViewModel
    {
        #region CONSTRUCTORS
        public Layer(MasterBeat masterBeat, string layername,  ObservableCollection<OSCValidation> oscvalidation, Mementor mementor) 
            : base (oscvalidation, mementor)
        {
            MessageAddress =  layername;

            LayerName = layername;           
            Enabled = false;
            BlendMode = ((BlendMode)0).ToString();

            Fade = new Slider(MessageAddress + nameof(Fade), oscvalidation, mementor);

            Content = new Content(masterBeat, MessageAddress, oscvalidation, mementor);
            Mask = new Mask(masterBeat, MessageAddress, oscvalidation, mementor);
            Coloration = new Coloration(MessageAddress, oscvalidation, mementor, masterBeat);
            PostFX = new PostFX(MessageAddress, oscvalidation, mementor);
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            Fade.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Fade)));
            Content.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Content)));
            Mask.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Mask)));
            Coloration.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Coloration)));
            PostFX.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(PostFX)));
        }
        #endregion

        #region PROPERTIES
        private string _layername;
        public string LayerName
        {
            get => _layername;
            set => SetAndNotify(ref _layername, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
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
                if(Mementor != null)
                    Mementor.PropertyChange(this, nameof(Out));
                SetAndNotify(ref _out, value);
                if (Out)
                    SendMessages(MessageAddress + nameof(Out), Out);
            }
        }

        private string _blendMode;
        public string BlendMode
        {
            get => _blendMode;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(BlendMode));                  
                SetAndNotify(ref _blendMode, value);
                SendMessages(MessageAddress + nameof(BlendMode), BlendMode);
            }
        }

        public Slider Fade { get; }
        public Content Content { get; }
        public Mask Mask { get; }
        public Coloration Coloration { get; }
        public PostFX PostFX{ get; }

        #endregion

        #region COPY/PASTE/LOAD
        public void Reset()
        {
            Enabled = false;
            BlendMode = ((BlendMode)0).ToString();

            Fade.Reset();
            Content.Reset();
            Mask.Reset();
            Coloration.Reset();
            PostFX.Reset();
        }

        public void Copy(LayerModel layermodel)
        {
            layermodel.MessageAddress = MessageAddress;
            layermodel.BlendMode = BlendMode;
            layermodel.LayerName = LayerName;
            layermodel.Name = Name;
            layermodel.ID = ID;
            Fade.Copy(layermodel.Fade);
            Content.Copy(layermodel.ContentModel);
            Mask.Copy(layermodel.maskmodel);
            Coloration.Copy(layermodel.ColorationModel);
            PostFX.Copy(layermodel.PostFXModel);
        }

        public void Paste(LayerModel layermodel)
        {
            DisabledMessages();

            MessageAddress = layermodel.MessageAddress;
            LayerName = layermodel.LayerName;
            Name = layermodel.Name;
            BlendMode = layermodel.BlendMode;
            Fade.Paste(layermodel.Fade);
            Out = layermodel.Out;
            ID = layermodel.ID;
            Content.Paste(layermodel.ContentModel);
            Mask.Paste(layermodel.maskmodel);
            Coloration.Paste(layermodel.ColorationModel);
            PostFX.Paste(layermodel.PostFXModel);

            EnabledMessages();
        }
        #endregion
    }
}