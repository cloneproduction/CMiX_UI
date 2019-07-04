using System;
using System.Collections.ObjectModel;
using CMiX.Models;
using CMiX.Services;
using Memento;

namespace CMiX.ViewModels
{
    public class GeometryFX : ViewModel
    {
        #region CONSTRUCTORS
        public GeometryFX(string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, ObservableCollection<OSCValidation> cansendmessage, Mementor mementor) : base(oscmessengers, cansendmessage, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(GeometryFX));
            Explode = new Slider(MessageAddress + nameof(Explode), oscmessengers, cansendmessage, mementor);  
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(GeometryFX));
            Explode.UpdateMessageAddress(MessageAddress + nameof(Explode));
        }
        #endregion

        #region PROPERTIES
        public Slider Explode { get; }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            DisabledMessages();
            Explode.Reset();
            EnabledMessages();
        }

        public void Copy(GeometryFXModel geometryFXdto)
        {
            Explode.Copy(geometryFXdto.Explode);
        }

        public void Paste(GeometryFXModel geometryFXdto)
        {
            DisabledMessages();
            Explode.Paste(geometryFXdto.Explode);
            EnabledMessages();
        }
        #endregion
    }
}