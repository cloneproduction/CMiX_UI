using CMiX.Services;
using System;

namespace CMiX.ViewModels
{
    public class Layer : ViewModel
    {
        public Layer(MasterBeat masterBeat, string layername, IMessenger messenger, int index)
        {
            Messenger = messenger;
            Index = index;
            LayerName = layername;
            Fade = 0.0;
            BlendMode = default;
            Index = 0;
            Enabled = false;
            BeatModifier = new BeatModifier(masterBeat, layername, messenger);
            Content = new Content(BeatModifier, layername, messenger);
            Mask = new Mask(BeatModifier, layername, messenger);
            Coloration = new Coloration(BeatModifier, layername, messenger);
        }

        public Layer(
            IMessenger messenger,
            string layername,
            bool enabled,
            int index,
            double fade,
            BlendMode blendMode,
            BeatModifier beatModifier,
            Content content,
            Mask mask,
            Coloration coloration)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
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

        public bool CanAcceptChildren { get; set; }

        private IMessenger Messenger { get; }

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
                Messenger.SendMessage("/" + LayerName + "/" + nameof(Fade), Fade);
            }
        }

        private int _index;
        public int Index
        {
            get => _index;
            set => SetAndNotify(ref _index, value);
        }

        private BlendMode _blendMode;
        public BlendMode BlendMode
        {
            get => _blendMode;
            set
            {
                SetAndNotify(ref _blendMode, value);
                Messenger.SendMessage("/" + LayerName + "/" + nameof(BlendMode), BlendMode);
            }
        }

        public BeatModifier BeatModifier { get; }

        public Content Content { get; }

        public Mask Mask { get; }

        public Coloration Coloration { get; }
    }
}
