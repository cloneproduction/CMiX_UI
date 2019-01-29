using CMiX.Models;
using CMiX.Services;
using GuiLabs.Undo;
using System;

namespace CMiX.ViewModels
{
    public class GeometryFX : ViewModel
    {
        #region CONSTRUCTORS
        public GeometryFX(string layername, OSCMessenger messenger, ActionManager actionmanager)
        : this
        (
            actionmanager: actionmanager,
            messenger: messenger,
            explode: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(GeometryFX), "Explode"), messenger, actionmanager),
            messageaddress: String.Format("{0}/{1}/", layername, nameof(GeometryFX))
        )
        {}

        public GeometryFX
            (
                ActionManager actionmanager,
                OSCMessenger messenger,
                Slider explode,
                string messageaddress
            )
            : base(actionmanager, messenger)
        {
            Explode = explode;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
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
            Messenger.SendEnabled = false;
            Explode.Paste(geometryFXdto.Explode);
            Messenger.SendEnabled = true;
        }
        #endregion
    }
}