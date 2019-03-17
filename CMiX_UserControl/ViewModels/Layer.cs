using System;
using System.Collections.ObjectModel;
using CMiX.Models;
using CMiX.Services;
using GuiLabs.Undo;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Layer : ViewModel
    {
        #region CONSTRUCTORS
        public Layer(MasterBeat masterBeat, string layername, ObservableCollection<OSCMessenger> messengers, int index, ActionManager actionmanager, Mementor mementor)
            : base (actionmanager, messengers)
        {
            Mementor = mementor;
            Messengers = messengers;
            MessageAddress = String.Format("{0}/", layername); 
            Index = index;
            LayerName = layername;       
            Index = 0;
            Enabled = false;
            BlendMode = ((BlendMode)0).ToString();
            Fade = new Slider(layername + "/Fade", messengers, actionmanager, mementor);
            BeatModifier = new BeatModifier(layername, messengers, masterBeat, actionmanager, mementor);
            Content = new Content(BeatModifier, layername, messengers, actionmanager, mementor);
            Mask = new Mask(BeatModifier, layername, messengers, actionmanager, mementor);
            Coloration = new Coloration(BeatModifier, layername, messengers, actionmanager, mementor);
            PostFX = new PostFX(layername, messengers, actionmanager, mementor);
            
        }

        public Layer
            (
                ObservableCollection<OSCMessenger> messengers,
                string messageaddress,
                string layername,
                bool enabled,
                int index,
                string blendMode,
                Slider fade,
                BeatModifier beatModifier,
                Content content,
                Mask mask,
                Coloration coloration,
                PostFX postfx,
                ActionManager actionmanager,
                Mementor mementor
            )
            : base (actionmanager, messengers)
        {
            Mementor = mementor;
            LayerName = layername;
            Index = index;
            Enabled = enabled;
            BlendMode = blendMode;
            Fade = fade ?? throw new ArgumentNullException(nameof(fade));
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Mask = mask ?? throw new ArgumentNullException(nameof(mask));
            Coloration = coloration ?? throw new ArgumentNullException(nameof(coloration));
            PostFX = postfx ?? throw new ArgumentNullException(nameof(postfx));
            Messengers = messengers;
            MessageAddress = messageaddress;
        }
        #endregion

        #region PROPERTIES
        //public bool CanAcceptChildren { get; set; }
        //public ObservableCollection<Layer> Children { get; private set; }

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
                Mementor.PropertyChange(this, "Out");
                //SetAndRecord(() => _out, value);
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