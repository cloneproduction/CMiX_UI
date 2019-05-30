using System;
using System.Collections.ObjectModel;
using CMiX.Models;
using CMiX.Services;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Layer : ViewModel
    {
        #region CONSTRUCTORS
        public Layer(MasterBeat masterBeat, string layername, ObservableCollection<OSCMessenger> messengers, int index, Mementor mementor) 
            : base (messengers, mementor)
        {
            MessageAddress =  layername;

            LayerName = layername;           
            Index = index;
            Index = 0;
            Enabled = false;
            BlendMode = ((BlendMode)0).ToString();

            Fade = new Slider(MessageAddress + nameof(Fade), messengers, mementor);
            BeatModifier = new BeatModifier(MessageAddress, messengers, masterBeat, mementor);
            Content = new Content(BeatModifier, MessageAddress, messengers, mementor);
            Mask = new Mask(BeatModifier, MessageAddress, messengers, mementor);
            Coloration = new Coloration(MessageAddress, messengers, mementor, BeatModifier);
            PostFX = new PostFX(MessageAddress, messengers, mementor);
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            Fade.UpdateMessageAddress(MessageAddress + nameof(Fade));
            BeatModifier.UpdateMessageAddress(MessageAddress);
            Content.UpdateMessageAddress(MessageAddress);
            Mask.UpdateMessageAddress(MessageAddress);
            Coloration.UpdateMessageAddress(MessageAddress);
            PostFX.UpdateMessageAddress(MessageAddress);
        }
        #endregion

        #region PROPERTIES

        private string _layername;
        public string LayerName
        {
            get => _layername;
            set => SetAndNotify(ref _layername, value);
        }

        private bool _enabled;
        public bool Enabled
        {
            get => _enabled;
            set => SetAndNotify(ref _enabled, value);
        }

        private int _index;
        public int Index
        {
            get => _index;
            set => SetAndNotify(ref _index, value);
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
        public BeatModifier BeatModifier { get; }
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
            BeatModifier.Reset();
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
            layermodel.Index = Index;
            Fade.Copy(layermodel.Fade);
            BeatModifier.Copy(layermodel.BeatModifierModel);
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
            BlendMode = layermodel.BlendMode;
            Fade.Paste(layermodel.Fade);
            Out = layermodel.Out;

            BeatModifier.Paste(layermodel.BeatModifierModel);
            Content.Paste(layermodel.ContentModel);
            Mask.Paste(layermodel.maskmodel);
            Coloration.Paste(layermodel.ColorationModel);
            PostFX.Paste(layermodel.PostFXModel);

            EnabledMessages();
        }

        public void Load(LayerModel layermodel)
        {
            DisabledMessages();

            BlendMode = layermodel.BlendMode;
            LayerName = layermodel.LayerName;
            Index = layermodel.Index;
            Out = layermodel.Out;
            Fade.Paste(layermodel.Fade);
            BeatModifier.Paste(layermodel.BeatModifierModel);
            Content.Paste(layermodel.ContentModel);
            Mask.Paste(layermodel.maskmodel);
            Coloration.Paste(layermodel.ColorationModel);
            PostFX.Paste(layermodel.PostFXModel);

            EnabledMessages();
        }
        #endregion
    }
}