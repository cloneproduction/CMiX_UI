using CMiX.Services;
using System;
using System.Windows;

namespace CMiX.ViewModels
{
    public class Layer : ViewModel
    {
        public Layer(MasterBeat masterBeat, string layername, IMessenger messenger)
        {
            Messenger = messenger;
            LayerName = layername;
            Fade = 0.0;
            BlendMode = default;
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
            double fade,
            BlendMode blendMode,
            BeatModifier beatModifier,
            Content content,
            Mask mask,
            Coloration coloration)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            LayerName = layername;
            Enabled = enabled;
            Fade = fade;
            BlendMode = blendMode;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Mask = mask ?? throw new ArgumentNullException(nameof(mask));
            Coloration = coloration ?? throw new ArgumentNullException(nameof(coloration));
        }

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
            set => SetAndNotify(ref _fade, value);
        }

        private BlendMode _blendMode;
        public BlendMode BlendMode
        {
            get => _blendMode;
            set => SetAndNotify(ref _blendMode, value);
        }

        public BeatModifier BeatModifier { get; }

        public Content Content { get; }

        public Mask Mask { get; }

        public Coloration Coloration { get; }
    }
}
