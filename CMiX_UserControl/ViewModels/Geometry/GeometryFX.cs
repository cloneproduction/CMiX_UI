using CMiX.Models;
using CMiX.Services;
using GuiLabs.Undo;
using System;
using System.Collections.ObjectModel;

namespace CMiX.ViewModels
{
    public class GeometryFX : ViewModel
    {
        #region CONSTRUCTORS
        public GeometryFX(string layername, ObservableCollection<OSCMessenger> messengers, ActionManager actionmanager)
        : this
        (
            actionmanager: actionmanager,
            messengers: messengers,
            explode: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(GeometryFX), "Explode"), messengers, actionmanager),
            messageaddress: String.Format("{0}/{1}/", layername, nameof(GeometryFX))
        )
        {}

        public GeometryFX
            (
                ActionManager actionmanager,
                ObservableCollection<OSCMessenger> messengers,
                Slider explode,
                string messageaddress
            )
            : base(actionmanager, messengers)
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