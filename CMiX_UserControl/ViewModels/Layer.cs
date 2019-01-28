using CMiX.Models;
using CMiX.Services;
using System;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Layer : ViewModel
    {
        #region CONSTRUCTORS
        public Layer(MasterBeat masterBeat, string layername, OSCMessenger messenger, int index, ActionManager actionmanager)
            : base (actionmanager, messenger)
        {
            Messenger = messenger;
            MessageAddress = String.Format("{0}/", layername); 
            Index = index;
            LayerName = layername;       
            Index = 0;
            Enabled = false;
            BlendMode = ((BlendMode)0).ToString();
            Fade = new Slider(layername + "/Fade", messenger, actionmanager);
            BeatModifier = new BeatModifier(layername, messenger, masterBeat, actionmanager);
            Content = new Content(BeatModifier, layername, messenger, actionmanager);
            Mask = new Mask(BeatModifier, layername, messenger, actionmanager);
            Coloration = new Coloration(BeatModifier, layername, messenger, actionmanager);
            LayerFX = new LayerFX(BeatModifier, layername, messenger, actionmanager);
        }

        public Layer
            (
                OSCMessenger messenger,
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
                LayerFX layerfx,
                ActionManager actionmanager
            )
            : base (actionmanager, messenger)
        {
            LayerName = layername;
            Index = index;
            Enabled = enabled;
            BlendMode = blendMode;
            Fade = fade ?? throw new ArgumentNullException(nameof(fade));
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Mask = mask ?? throw new ArgumentNullException(nameof(mask));
            Coloration = coloration ?? throw new ArgumentNullException(nameof(coloration));
            LayerFX = layerfx ?? throw new ArgumentNullException(nameof(layerfx));
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
        }
        #endregion

        #region PROPERTIES
        public string MessageAddress { get; set; }

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
                SetAndRecord(() => _out, value);
                SetAndNotify(ref _out, value);
                if (Out)
                    Messenger.SendMessage(MessageAddress + nameof(Out), Out);
            }
        }

        private string _blendMode;
        [OSC]
        public string BlendMode
        {
            get => _blendMode;
            set
            {
                SetAndNotify(ref _blendMode, value);
                Messenger.SendMessage(MessageAddress + nameof(BlendMode), BlendMode);
            }
        }
        public Slider Fade { get; }
        public BeatModifier BeatModifier { get; }
        public Content Content { get; }
        public Mask Mask { get; }
        public Coloration Coloration { get; }
        public LayerFX LayerFX{ get; }
        
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
            LayerFX.Copy(layerdto.LayerFXDTO);
        }

        public void Paste(LayerDTO layerdto)
        {
            Messenger.SendEnabled = false;

            BlendMode = layerdto.BlendMode;
            Fade.Paste(layerdto.Fade);
            Out = layerdto.Out;
            BeatModifier.Paste(layerdto.BeatModifierDTO);
            Content.Paste(layerdto.ContentDTO);
            Mask.Paste(layerdto.MaskDTO);
            Coloration.Paste(layerdto.ColorationDTO);
            LayerFX.Paste(layerdto.LayerFXDTO);

            Messenger.SendEnabled = true;
        }

        public void Load(LayerDTO layerdto)
        {
            Messenger.SendEnabled = false;

            BlendMode = layerdto.BlendMode;
            LayerName = layerdto.LayerName;
            Index = layerdto.Index;
            Out = layerdto.Out;
            Fade.Paste(layerdto.Fade);
            BeatModifier.Paste(layerdto.BeatModifierDTO);
            Content.Paste(layerdto.ContentDTO);
            Mask.Paste(layerdto.MaskDTO);
            Coloration.Paste(layerdto.ColorationDTO);
            LayerFX.Paste(layerdto.LayerFXDTO);

            Messenger.SendEnabled = true;
        }
        #endregion
    }
}