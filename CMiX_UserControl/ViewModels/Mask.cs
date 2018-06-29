using System;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class Mask : ViewModel, IMessengerData
    {
        public Mask(Beat masterbeat, string layername, IMessenger messenger)
            : this(
                messenger: messenger,
                messageaddress: String.Format("{0}/{1}/", layername, nameof(Mask)),
                enable: false,
                layerName: layername,
                beatModifier: new BeatModifier(String.Format("{0}/{1}/", layername, nameof(Mask)), messenger, masterbeat),
                geometry: new Geometry(String.Format("{0}/{1}/", layername, nameof(Mask)), messenger),
                texture: new Texture(String.Format("{0}/{1}/", layername, nameof(Mask)), messenger),
                postFX: new PostFX(String.Format("{0}/{1}/", layername, nameof(Mask)), messenger))
        {}

        public Mask(
            IMessenger messenger,
            string messageaddress,
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
            MessageAddress = messageaddress;
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
                Messenger.SendMessage(MessageAddress + nameof(Enable), Enable);
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