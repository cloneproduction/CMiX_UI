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
        public GeometryTranslate(string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) 
            : base (oscmessengers, mementor)
        {
            MessageAddress = String.Format("{0}/", messageaddress);
            Mode = default;
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
        }
        #endregion

        #region PROPERTIES
        private GeometryTranslateMode _Mode;
        public GeometryTranslateMode Mode
        {
            get => _Mode;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, nameof(Mode));
                SetAndNotify(ref _Mode, value);
                SendMessages(MessageAddress + nameof(Mode), Mode);
            }
        }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            DisabledMessages();
            Mode = default;
            EnabledMessages();
        }

        public void Copy(GeometryTranslateModel geometrytranslatemodel)
        {
            geometrytranslatemodel.MessageAddress = MessageAddress;
            geometrytranslatemodel.Mode = Mode;
        }

        public void Paste(GeometryTranslateModel geometrytranslatemodel)
        {
            DisabledMessages();
            MessageAddress = geometrytranslatemodel.MessageAddress;
            Mode = geometrytranslatemodel.Mode;
            EnabledMessages();
        }
        #endregion
    }
}