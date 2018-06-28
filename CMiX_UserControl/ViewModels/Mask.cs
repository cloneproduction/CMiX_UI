using System;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class Mask : ViewModel, IMessengerData
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
            MessageAddress = LayerName + "/" + nameof(Mask);
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            Geometry = geometry ?? throw new ArgumentNullException(nameof(geometry));
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            PostFX = postFX ?? throw new ArgumentNullException(nameof(postFX));
        }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }


        private bool _enable;
        [OSC]
        public bool Enable
        {
            get => _enable;
            set
            {
                SetAndNotify(ref _enable, value);
                Messenger.SendMessage(MessageAddress + "/" + nameof(Enable), Enable);
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