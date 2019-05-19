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
        public GeometryFX(string layername, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) : base(oscmessengers, mementor)
        {
            Explode = new Slider(String.Format("{0}/{1}/{2}", layername, nameof(GeometryFX), "Explode"), oscmessengers, mementor);
            MessageAddress = String.Format("{0}/{1}/", layername, nameof(GeometryFX));
        }
        #endregion

        #region PROPERTIES
        public Slider Explode { get; }
        #endregion

        #region COPY/PASTE
        public void Copy(GeometryFXDTO geometryFXdto)
        {
            Explode.Copy(geometryFXdto.Explode);
        }

        public void Paste(GeometryFXDTO geometryFXdto)
        {
            DisabledMessages();
            Explode.Paste(geometryFXdto.Explode);
            EnabledMessages();
        }
        #endregion
    }
}