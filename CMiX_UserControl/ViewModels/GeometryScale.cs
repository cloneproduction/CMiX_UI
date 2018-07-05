using System;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class GeometryScale : ViewModel, IMessengerData
    {
        public GeometryScale(string layername, IMessenger messenger)
            : this(
                  messageaddress: String.Format("{0}/{1}", layername, nameof(ScaleMode)),
                  messenger: messenger,
                  scaleMode: default
                  )
        {
        }

        public GeometryScale
            (
                string messageaddress,
                IMessenger messenger,
                GeometryScaleMode scaleMode
            )
        {
            MessageAddress = messageaddress;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public IMessenger Messenger { get; }

        private GeometryScaleMode _ScaleMode;
        [OSC]
        public GeometryScaleMode ScaleMode
        {
            get => _ScaleMode;
            set
            {
                SetAndNotify(ref _ScaleMode, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress, ScaleMode);
            }
        }
    }
}