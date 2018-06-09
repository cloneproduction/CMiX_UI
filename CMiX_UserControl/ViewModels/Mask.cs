using System;

namespace CMiX.ViewModels
{
    public class Mask : ViewModel
    {
        public Mask(Beat masterbeat, string layername)
            : this(
                  enable: true,
                  layerName : layername,
                  beatModifier: new BeatModifier(masterbeat, layername, nameof(Mask)),
                  geometry: new Geometry(layername, nameof(Mask)),
                  texture: new Texture(layername, nameof(Mask)),
                  postFX: new PostFX(layername))
        { }

        public Mask(bool enable, string layerName, BeatModifier beatModifier, Geometry geometry, Texture texture, PostFX postFX)
        {
            Enable = enable;
            LayerName = layerName;
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

        private string _layerName;
        public string LayerName
        {
            get => _layerName;
            set => SetAndNotify(ref _layerName, value);
        }

        public BeatModifier BeatModifier { get; }

        public Geometry Geometry { get; }

        public Texture Texture { get; }

        public PostFX PostFX { get; }
    }
}
