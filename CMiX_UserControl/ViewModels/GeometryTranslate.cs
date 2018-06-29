using System;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class GeometryTranslate : ViewModel, IMessengerData
    {
        public GeometryTranslate(string layername, IMessenger messenger)
            : this(
                  messageaddress: String.Format("{0}/{1}/", layername, nameof(Content)),
                  messenger: messenger,
                  translateMode: default
                  )
        {
        }

        public GeometryTranslate
            (
                string messageaddress,
                IMessenger messenger,
                GeometryTranslateMode translateMode
            )
        {
            MessageAddress = messageaddress;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public IMessenger Messenger { get; }

        private GeometryTranslateMode _TranslateMode;
        [OSC]
        public GeometryTranslateMode TranslateMode
        {
            get => _TranslateMode;
            set
            {
                SetAndNotify(ref _TranslateMode, value);
                Messenger.SendMessage(MessageAddress + nameof(TranslateMode), TranslateMode);
            }
        }
    }
}
