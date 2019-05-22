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
        public GeometryFX(string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) : base(oscmessengers, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(GeometryFX));
            Explode = new Slider(MessageAddress + nameof(Explode), oscmessengers, mementor);  
        }
        #endregion

        #region PROPERTIES
        public Slider Explode { get; }
        #endregion

        #region COPY/PASTE
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