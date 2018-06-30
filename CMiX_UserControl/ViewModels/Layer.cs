using CMiX.Services;
using System;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Layer : ViewModel, IMessengerData
    {
        public Layer(MasterBeat masterBeat, string layername, IMessenger messenger, int index)
        {
            Messenger = messenger;
            Index = index;
            LayerName = layername;
            MessageAddress = layername;
            Fade = 0.0;
            BlendMode = ((BlendMode)0).ToString();
            Index = 0;
            Enabled = false;
            BeatModifier = new BeatModifier(layername, messenger, masterBeat);
            Content = new Content(BeatModifier, layername, messenger);
            Mask = new Mask(BeatModifier, layername, messenger);
            Coloration = new Coloration(BeatModifier, layername, messenger);
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
            Coloration coloration)
        {

            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            LayerName = layername;
            Index = index;
            Enabled = enabled;
            Fade = fade;
            BlendMode = blendMode;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Mask = mask ?? throw new ArgumentNullException(nameof(mask));
            Coloration = coloration ?? throw new ArgumentNullException(nameof(coloration));
        }

        private IMessenger Messenger { get; }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

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

        private double _fade;
        public double Fade
        {
            get => _fade;
            set
            {
                SetAndNotify(ref _fade, value);
                Messenger.SendMessage(MessageAddress + "/" + nameof(Fade), Fade);
            }
        }

        private int _index;
        public int Index
        {
            get => _index;
            set => SetAndNotify(ref _index, value);
        }

        private string _blendMode;
        public string BlendMode
        {
            get => _blendMode;
            set
            {
                SetAndNotify(ref _blendMode, value);
                Messenger.SendMessage(MessageAddress + "/" + nameof(BlendMode), BlendMode);
            }
        }

        public BeatModifier BeatModifier { get; }

        public Content Content { get; }

        public Mask Mask { get; }

        public Coloration Coloration { get; }
    }
}