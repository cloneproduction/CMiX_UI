using System;
using System.Windows;

namespace CMiX.ViewModels
{
    public class Content : ViewModel
    {
        public Content(Beat masterBeat, string layerName)
            : this(
                  layerName: layerName,
                  enable: true,
                  beatModifier: new BeatModifier(masterBeat),
                  geometry: new Geometry(layerName),
                  texture: new Texture(),
                  postFX: new PostFX())
        { }

        public Content(bool enable, string layerName, BeatModifier beatModifier, Geometry geometry, Texture texture, PostFX postFX)
        {
            LayerName = layerName;
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
