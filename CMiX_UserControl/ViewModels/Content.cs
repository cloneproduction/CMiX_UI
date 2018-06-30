using System;
using CMiX.Services;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Content : ViewModel, IMessengerData
    {
        public Content(Beat masterbeat, string layername, IMessenger messenger)
            : this(
                  enable: true,
                  messageaddress: String.Format("{0}/{1}/", layername, nameof(Content)),
                  messenger: messenger,
                  beatModifier: new BeatModifier(String.Format("{0}/{1}", layername, nameof(Content)), messenger, masterbeat),
                  geometry: new Geometry(String.Format("{0}/{1}", layername, nameof(Content)), messenger),
                  texture: new Texture(String.Format("{0}/{1}", layername, nameof(Content)), messenger),
                  postFX: new PostFX(String.Format("{0}/{1}", layername, nameof(Content)), messenger))
        { }

        public Content(
            bool enable,
            string messageaddress,
            IMessenger messenger, 
            BeatModifier beatModifier, 
            Geometry geometry, 
            Texture texture, 
            PostFX postFX)
        {
            Enable = enable;
            MessageAddress = messageaddress;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            Geometry = geometry ?? throw new ArgumentNullException(nameof(geometry));
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            PostFX = postFX ?? throw new ArgumentNullException(nameof(postFX));
        }

        public IMessenger Messenger { get; }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        private bool _enable;
        public bool Enable
        {
            get => _enable;
            set
            {
                SetAndNotify(ref _enable, value);
            }
        }

        public BeatModifier BeatModifier { get; }

        public Geometry Geometry { get; }

        public Texture Texture { get; }

        public PostFX PostFX { get; }
    }
}