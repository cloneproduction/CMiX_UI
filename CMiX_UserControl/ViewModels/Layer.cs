using CMiX.Models;
using CMiX.Services;
using System;
using MonitoredUndo;
using System.Collections.ObjectModel;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Layer : ViewModel, IMessengerData
    {
        #region CONSTRUCTORS

        public Layer(MasterBeat masterBeat, string layername, IMessenger messenger, int index)
        {
            Messenger = messenger;
            MessageAddress = String.Format("{0}/", layername);
            MessageEnabled = false;
            Index = index;
            LayerName = layername;
            Fade = 0.0;
            BlendMode = ((BlendMode)0).ToString();
            Index = 0;
            Enabled = false;
            BeatModifier = new BeatModifier(layername, messenger, masterBeat);
            Content = new Content(BeatModifier, layername, messenger);
            Mask = new Mask(BeatModifier, layername, messenger);
            Coloration = new Coloration(BeatModifier, layername, messenger);
            LayerFX = new LayerFX(BeatModifier, layername, messenger);
            MessageEnabled = true;
        }

        public Layer(
            IMessenger messenger,
            string messageaddress,
            string layername,
            bool enabled,
            int index,
            double fade,
            string blendMode,
            BeatModifier beatModifier,
            Content content,
            Mask mask,
            Coloration coloration,
            LayerFX layerfx,
            bool messageEnabled
            )
        {
            LayerName = layername;
            Index = index;
            Enabled = enabled;
            Fade = fade;
            BlendMode = blendMode;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Mask = mask ?? throw new ArgumentNullException(nameof(mask));
            Coloration = coloration ?? throw new ArgumentNullException(nameof(coloration));
            LayerFX = layerfx ?? throw new ArgumentNullException(nameof(layerfx));
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
        }

        #endregion

        #region PROPERTY
        public bool CanAcceptChildren { get; set; }
        public ObservableCollection<Layer> Children { get; private set; }

        public IMessenger Messenger { get; }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }


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

        private double _fade;
        [OSC]
        public double Fade
        {
            get => _fade;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, "Fade", _fade, value, "Fade Changed");
                SetAndNotify(ref _fade, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Fade), Fade);
            }
        }

        private bool _out;
        [OSC]
        public bool Out
        {
            get => _out;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, "Out", _out, value, "Out Changed");
                SetAndNotify(ref _out, value);
                if (MessageEnabled && Out)
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
                DefaultChangeFactory.Current.OnChanging(this, "BlendMode", _blendMode, value, "BlendMode Changed");
                SetAndNotify(ref _blendMode, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(BlendMode), BlendMode);
            }
        }

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
            layerdto.Fade = Fade;
            layerdto.LayerName = LayerName;
            layerdto.Index = Index;

            BeatModifier.Copy(layerdto.BeatModifierDTO);
            Content.Copy(layerdto.ContentDTO);
            Mask.Copy(layerdto.MaskDTO);
            Coloration.Copy(layerdto.ColorationDTO);
            LayerFX.Copy(layerdto.LayerFXDTO);
        }

        public void Paste(LayerDTO layerdto)
        {
            MessageEnabled = false;

            BlendMode = layerdto.BlendMode;
            Fade = layerdto.Fade;
            Out = layerdto.Out;

            BeatModifier.Paste(layerdto.BeatModifierDTO);
            Content.Paste(layerdto.ContentDTO);
            Mask.Paste(layerdto.MaskDTO);
            Coloration.Paste(layerdto.ColorationDTO);
            LayerFX.Paste(layerdto.LayerFXDTO);

            MessageEnabled = true;
        }

        public void Load(LayerDTO layerdto)
        {
            MessageEnabled = false;

            BlendMode = layerdto.BlendMode;
            Fade = layerdto.Fade;
            LayerName = layerdto.LayerName;
            Index = layerdto.Index;
            Out = layerdto.Out;

            BeatModifier.Paste(layerdto.BeatModifierDTO);
            Content.Paste(layerdto.ContentDTO);
            Mask.Paste(layerdto.MaskDTO);
            Coloration.Paste(layerdto.ColorationDTO);
            LayerFX.Paste(layerdto.LayerFXDTO);

            MessageEnabled = true;
        }
        #endregion
    }
}