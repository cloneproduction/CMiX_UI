using CMiX.Models;
using CMiX.Services;
using GuiLabs.Undo;
using System;

namespace CMiX.ViewModels
{
    public class GeometryFX : ViewModel, IMessengerData
    {
        public GeometryFX(string layername, IMessenger messenger, ActionManager actionmanager)
            : this
            (
                actionmanager: actionmanager,
                explode: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(GeometryFX), "Explode"), messenger, actionmanager),
                messageaddress: String.Format("{0}/{1}/", layername, nameof(GeometryFX)),
                messenger: messenger,
                messageEnabled: true
            )
        { }

        public GeometryFX
            (
                ActionManager actionmanager,
                Slider explode,
                IMessenger messenger,
                string messageaddress,
                bool messageEnabled
            )
            : base (actionmanager)
        {
            Explode = explode;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
        }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public IMessenger Messenger { get; }

        public Slider Explode { get; }

        public void Copy(GeometryFXDTO geometryFXdto)
        {
            //geometryFXdto.Amount = Amount;
        }

        public void Paste(GeometryFXDTO geometryFXdto)
        {
            MessageEnabled = false;
            //Amount = geometryFXdto.Amount;
            MessageEnabled = true;
        }
    }
}