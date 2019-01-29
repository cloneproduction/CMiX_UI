using System;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    public class GeometryTranslate : ViewModel
    {
        public GeometryTranslate(string layername, OSCMessenger messenger, ActionManager actionmanager)
            : this
            (
                actionmanager: actionmanager,
                messageaddress: layername + "/",
                messenger: messenger,
                translateMode: default
            )
        {
        }

        public GeometryTranslate
            (
                ActionManager actionmanager,
                string messageaddress,
                OSCMessenger messenger,
                GeometryTranslateMode translateMode
            )
            : base (actionmanager)
        {
            MessageAddress = messageaddress;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

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

        public void Copy(GeometryTranslateDTO geometrytranslatedto)
        {
            geometrytranslatedto.TranslateModeDTO = TranslateMode;
        }

        public void Paste(GeometryTranslateDTO geometrytranslatedto)
        {
            Messenger.SendEnabled = false;
            TranslateMode = geometrytranslatedto.TranslateModeDTO;
            Messenger.SendEnabled = true;
        }
    }
}
