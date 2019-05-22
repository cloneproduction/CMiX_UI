using System;
using CMiX.Services;
using CMiX.Models;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class GeometryTranslate : ViewModel
    {
        #region CONSTRUCTORS
        public GeometryTranslate(string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) : base (oscmessengers, mementor)
        {
            MessageAddress = messageaddress;
            TranslateMode = default;
        }
        #endregion

        #region PROPERTIES
        private GeometryTranslateMode _TranslateMode;
        public GeometryTranslateMode TranslateMode
        {
            get => _TranslateMode;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, nameof(TranslateMode));
                SetAndNotify(ref _TranslateMode, value);
                SendMessages(MessageAddress + nameof(TranslateMode), TranslateMode);
            }
        }
        #endregion

        #region COPY/PASTE
        public void Copy(GeometryTranslateModel geometrytranslatemodel)
        {
            geometrytranslatemodel.MessageAddress = MessageAddress;
            geometrytranslatemodel.TranslateMode = TranslateMode;
        }

        public void Paste(GeometryTranslateModel geometrytranslatemodel)
        {
            DisabledMessages();
            MessageAddress = geometrytranslatemodel.MessageAddress;
            TranslateMode = geometrytranslatemodel.TranslateMode;
            EnabledMessages();
        }
        #endregion
    }
}