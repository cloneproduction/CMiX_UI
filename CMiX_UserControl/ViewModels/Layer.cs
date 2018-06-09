using System;
using System.Windows;

namespace CMiX.ViewModels
{
    public class Layer : ViewModel
    {
        public Layer(MasterBeat masterBeat, string name)
        {
            Name = name;
            Fade = 0.0;
            BlendMode = default;
            BeatModifier = new BeatModifier(masterBeat, name, "Master");
            Content = new Content(BeatModifier, name);
            Mask = new Mask(BeatModifier, name);
            Coloration = new Coloration(BeatModifier, name);
        }

        public Layer(
            string name,
            double fade,
            BlendMode blendMode,
            BeatModifier beatModifier,
            Content content,
            Mask mask,
            Coloration coloration)
        {
            Name = name;
            Fade = fade;
            BlendMode = blendMode;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Mask = mask ?? throw new ArgumentNullException(nameof(mask));
            Coloration = coloration ?? throw new ArgumentNullException(nameof(coloration));
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private double _enabled;
        public double Enabled
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
