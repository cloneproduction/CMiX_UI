﻿using System;
using CMiX.Services;
using CMiX.Models;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class GeometryTranslate : ViewModel
    {
        #region CONSTRUCTORS
        public GeometryTranslate(string layername, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) : base (oscmessengers, mementor)
        {
            MessageAddress = layername + "/";
            TranslateMode = default;
        }
        #endregion

        #region PROPERTIES
        private GeometryTranslateMode _TranslateMode;
        [OSC]
        public GeometryTranslateMode TranslateMode
        {
            get => _TranslateMode;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, "TranslateMode");
                SetAndNotify(ref _TranslateMode, value);
                SendMessages(MessageAddress + nameof(TranslateMode), TranslateMode);
            }
        }
        #endregion

        #region COPY/PASTE
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
        #endregion
    }
}