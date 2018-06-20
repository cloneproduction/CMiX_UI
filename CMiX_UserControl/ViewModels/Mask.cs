using System;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class Mask : ViewModel
    {
        public Mask(Beat masterbeat, string layername, IMessenger messenger)
            : this(
                  messenger: messenger,
                  enable: false,
                  layerName: layername,
                  beatModifier: new BeatModifier(masterbeat, layername + "/" + nameof(Mask), messenger),
                  geometry: new Geometry(layername + "/" + nameof(Mask), messenger),
                  texture: new Texture(layername + "/" + nameof(Mask), messenger),
                  postFX: new PostFX(layername + "/" + nameof(Mask), messenger))
        {}

        public Mask(
            IMessenger messenger,
            string layerName,
            bool enable, 
            BeatModifier beatModifier, 
            Geometry geometry, 
            Texture texture, 
            PostFX postFX)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
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
            set
            {
                SetAndNotify(ref _enable, value);
                Messenger.SendMessage(LayerName + "/" + nameof(Mask) + "/" + nameof(Enable), Enable);
            }
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