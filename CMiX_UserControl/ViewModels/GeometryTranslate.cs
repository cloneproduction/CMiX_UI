using System;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class GeometryTranslate : ViewModel, IMessengerData
    {
        public GeometryTranslate(string layername, IMessenger messenger)
            : this(
                  messageaddress: layername + "/",
                  messenger: messenger,
                  messageEnabled : true,
                  translateMode: default
                  )
        {
        }

        public GeometryTranslate
            (
                string messageaddress,
                bool messageEnabled,
                IMessenger messenger,
                GeometryTranslateMode translateMode
            )
        {
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
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
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(TranslateMode), TranslateMode);
            }
        }
    }
}
