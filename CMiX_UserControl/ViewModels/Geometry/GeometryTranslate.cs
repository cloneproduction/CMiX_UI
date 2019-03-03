using System;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;
using System.Collections.ObjectModel;

namespace CMiX.ViewModels
{
    public class GeometryTranslate : ViewModel
    {
        public GeometryTranslate(string layername, ObservableCollection<OSCMessenger> messengers, ActionManager actionmanager)
            : this
            (
                actionmanager: actionmanager,
                messageaddress: layername + "/",
                messengers: messengers,
                translateMode: default
            )
        {
        }

        public GeometryTranslate
            (
                ActionManager actionmanager,
                string messageaddress,
                ObservableCollection<OSCMessenger> messengers,
                GeometryTranslateMode translateMode
            )
            : base (actionmanager)
        {
            MessageAddress = messageaddress;
            Messengers = messengers ?? throw new ArgumentNullException(nameof(messengers));
        }

        private GeometryTranslateMode _TranslateMode;
        [OSC]
        public GeometryTranslateMode TranslateMode
        {
            get => _TranslateMode;
            set
            {
                SetAndNotify(ref _TranslateMode, value);
                SendMessages(MessageAddress + nameof(TranslateMode), TranslateMode);
            }
        }

        public void Copy(GeometryTranslateDTO geometrytranslatedto)
        {
            geometrytranslatedto.TranslateModeDTO = TranslateMode;
        }

        public void Paste(GeometryTranslateDTO geometrytranslatedto)
        {
            DisabledMessages();
            TranslateMode = geometrytranslatedto.TranslateModeDTO;
            EnabledMessages();
        }
    }
}
