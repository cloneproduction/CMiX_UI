using System;
using CMiX.Services;
using CMiX.Models;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class GeometryScale : ViewModel
    {
        #region CONSTRUCTORS
        public GeometryScale(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor) 
            : base (oscvalidation, mementor)
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
        private GeometryScaleMode _Mode;
        public GeometryScaleMode Mode
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

        public void Copy(GeometryScaleModel geometryscalemodel)
        {
            geometryscalemodel.MessageAddress = MessageAddress;
            geometryscalemodel.Mode = Mode;
        }

        public void Paste(GeometryScaleModel geometryscalemodel)
        {
            DisabledMessages();

            MessageAddress = geometryscalemodel.MessageAddress;
            Mode = geometryscalemodel.Mode;

            EnabledMessages();
        }
        #endregion
    }
}