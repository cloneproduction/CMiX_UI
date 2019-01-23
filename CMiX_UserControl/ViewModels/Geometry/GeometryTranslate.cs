using System;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    public class GeometryTranslate : ViewModel, IMessengerData
    {
        public GeometryTranslate(string layername, IMessenger messenger, ActionManager actionmanager)
            : this
            (
                actionmanager: actionmanager,
                messageaddress: layername + "/",
                messenger: messenger,
                messageEnabled : true,
                translateMode: default
            )
        {
        }

        public GeometryTranslate
            (
                ActionManager actionmanager,
                string messageaddress,
                bool messageEnabled,
                IMessenger messenger,
                GeometryTranslateMode translateMode
            )
            : base (actionmanager)
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

        public void Copy(GeometryTranslateDTO geometrytranslatedto)
        {
            geometrytranslatedto.TranslateModeDTO = TranslateMode;
        }

        public void Paste(GeometryTranslateDTO geometrytranslatedto)
        {
            MessageEnabled = false;
            TranslateMode = geometrytranslatedto.TranslateModeDTO;
            MessageEnabled = true;
        }
    }
}
