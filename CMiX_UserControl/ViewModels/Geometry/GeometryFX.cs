using CMiX.Models;
using CMiX.Services;
using System;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class GeometryFX : ViewModel
    {
        #region CONSTRUCTORS
        public GeometryFX(string layername, ObservableCollection<OSCMessenger> messengers, Mementor mementor)
        : this
        (
            messengers: messengers,
            explode: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(GeometryFX), "Explode"), messengers, mementor),
            messageaddress: String.Format("{0}/{1}/", layername, nameof(GeometryFX))
        )
        {}

        public GeometryFX
            (
                ObservableCollection<OSCMessenger> messengers,
                Slider explode,
                string messageaddress
            )
            : base(messengers)
        {
            Explode = explode;
            Messengers = messengers ?? throw new ArgumentNullException(nameof(messengers));
            MessageAddress = messageaddress;
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