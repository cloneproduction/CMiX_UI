namespace CMiX.ViewModels
{
    public class Layer : ViewModel
    {
        public Layer()
            : this(
                  name: string.Empty,
                  fade: 0.0,
                  blendMode: default,
                  beatModifier: new BeatModifier(),
                  content: new Content(),
                  mask: new Mask(),
                  coloration: new Coloration())
        { }

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
            BeatModifier = beatModifier ?? throw new System.ArgumentNullException(nameof(beatModifier));
            Content = content ?? throw new System.ArgumentNullException(nameof(content));
            Mask = mask ?? throw new System.ArgumentNullException(nameof(mask));
            Coloration = coloration ?? throw new System.ArgumentNullException(nameof(coloration));
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
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
