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
            MessageAddress = String.Format("{0}/", layername);
            LayerName = layername;           
            Index = index;
            Index = 0;
            Enabled = false;
            BlendMode = ((BlendMode)0).ToString();
            Fade = new Slider(layername + "/Fade", messengers, mementor);
            BeatModifier = new BeatModifier(layername, messengers, masterBeat, mementor);
            Content = new Content(BeatModifier, layername, messengers, mementor);
            Mask = new Mask(BeatModifier, layername, messengers, mementor);
            Coloration = new Coloration(BeatModifier, layername, messengers, mementor);
            PostFX = new PostFX(layername, messengers, mementor);
        }
        #endregion

        #region PROPERTIES

        private string _layername;
        [OSC]
        public string LayerName
        {
            get => _layername;
            set => SetAndNotify(ref _layername, value);
        }

        private bool _enabled;
        [OSC]
        public bool Enabled
        {
            get => _enabled;
            set => SetAndNotify(ref _enabled, value);
        }

        private int _index;
        [OSC]
        public int Index
        {
            get => _index;
            set => SetAndNotify(ref _index, value);
        }

        private bool _out;
        [OSC]
        public bool Out
        {
            get => _out;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, "Out");
                SetAndNotify(ref _out, value);
                if (Out)
                    SendMessages(MessageAddress + nameof(Out), Out);
            }
        }

        private string _blendMode;
        [OSC]
        public string BlendMode
        {
            get => _blendMode;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, "BlendMode");                  
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
        public void Copy(LayerDTO layerdto)
        {
            layerdto.BlendMode = BlendMode;
            layerdto.LayerName = LayerName;
            layerdto.Index = Index;
            Fade.Copy(layerdto.Fade);
            BeatModifier.Copy(layerdto.BeatModifierDTO);
            Content.Copy(layerdto.ContentDTO);
            Mask.Copy(layerdto.MaskDTO);
            Coloration.Copy(layerdto.ColorationDTO);
            PostFX.Copy(layerdto.PostFXDTO);
        }

        public void Paste(LayerDTO layerdto)
        {
            DisabledMessages();

            BlendMode = layerdto.BlendMode;
            Fade.Paste(layerdto.Fade);
            Out = layerdto.Out;
            BeatModifier.Paste(layerdto.BeatModifierDTO);
            Content.Paste(layerdto.ContentDTO);
            Mask.Paste(layerdto.MaskDTO);
            Coloration.Paste(layerdto.ColorationDTO);
            PostFX.Paste(layerdto.PostFXDTO);

            EnabledMessages();
        }

        public void Load(LayerDTO layerdto)
        {
            DisabledMessages();

            BlendMode = layerdto.BlendMode;
            LayerName = layerdto.LayerName;
            Index = layerdto.Index;
            Out = layerdto.Out;
            Fade.Paste(layerdto.Fade);
            BeatModifier.Paste(layerdto.BeatModifierDTO);
            Content.Paste(layerdto.ContentDTO);
            Mask.Paste(layerdto.MaskDTO);
            Coloration.Paste(layerdto.ColorationDTO);
            PostFX.Paste(layerdto.PostFXDTO);

            EnabledMessages();
        }
        #endregion
    }
}