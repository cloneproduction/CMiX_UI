using CMiX.Models;
using CMiX.Services;
using GuiLabs.Undo;
using System;

namespace CMiX.ViewModels
{
    public class GeometryFX : ViewModel, IMessengerData
    {
        #region CONSTRUCTORS
        public GeometryFX(string layername, IMessenger messenger, ActionManager actionmanager)
        : this
        (
            actionmanager: actionmanager,
            explode: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(GeometryFX), "Explode"), messenger, actionmanager),
            messageaddress: String.Format("{0}/{1}/", layername, nameof(GeometryFX)),
            messenger: messenger,
            messageEnabled: true
        )
        {}

        public GeometryFX
            (
                ActionManager actionmanager,
                Slider explode,
                IMessenger messenger,
                string messageaddress,
                bool messageEnabled
            )
            : base(actionmanager)
        {
            Explode = explode;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
        }
        #endregion

        #region PROPERTIES
        public string MessageAddress { get; set; }
        public bool MessageEnabled { get; set; }
        public IMessenger Messenger { get; }

        public Slider Explode { get; }
        #endregion

        #region COPY/PASTE
        public void Copy(GeometryFXDTO geometryFXdto)
        {
            Explode.Copy(geometryFXdto.Explode);
        }

        public void Paste(GeometryFXDTO geometryFXdto)
        {
            MessageEnabled = false;
            Explode.Paste(geometryFXdto.Explode);
            MessageEnabled = true;
        }
        #endregion
    }
}