using System;

namespace CMiX.ViewModels
{
    public class Content : ViewModel
    {
        public Content(Beat masterBeat)
            : this(
                  enable: true,
                  beatModifier: new BeatModifier(masterBeat),
                  geometry: new Geometry(),
                  texture: new Texture(),
                  postFX: new PostFX())
        { }

        public Content(bool enable, BeatModifier beatModifier, Geometry geometry, Texture texture, PostFX postFX)
        {
            Enable = enable;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            Geometry = geometry ?? throw new ArgumentNullException(nameof(geometry));
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            PostFX = postFX ?? throw new ArgumentNullException(nameof(postFX));
        }

        private bool _enable;
        public bool Enable
        {
            get => _enable;
            set => SetAndNotify(ref _enable, value);
        }

        public BeatModifier BeatModifier { get; }

        public Geometry Geometry { get; }

        public Texture Texture { get; }

        public PostFX PostFX { get; }
    }
}
