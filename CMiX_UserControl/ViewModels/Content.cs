using System;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class Content : ViewModel
    {
        public Content(Beat masterbeat, string layername, IMessenger messenger)
            : this(
                  enable: true,
                  layerName: layername,
                  messenger: messenger,
                  beatModifier: new BeatModifier(masterbeat, layername + "/" + nameof(Content), messenger),
                  geometry: new Geometry(layername + "/" + nameof(Content), messenger),
                  texture: new Texture(layername + "/" + nameof(Content), messenger),
                  postFX: new PostFX(layername + "/" + nameof(Content), messenger))
        { }

        public Content(bool enable, IMessenger messenger, string layerName, BeatModifier beatModifier, Geometry geometry, Texture texture, PostFX postFX)
        {
            Enable = enable;
            LayerName = layerName;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
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
        public IMessenger Messenger { get; }
        public BeatModifier BeatModifier { get; }

        public Geometry Geometry { get; }

        public Texture Texture { get; }

        public PostFX PostFX { get; }
    }
}
