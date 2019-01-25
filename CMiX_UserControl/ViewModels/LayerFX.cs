using CMiX.Models;
using CMiX.Services;
using System;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    public class LayerFX : ViewModel, IMessengerData
    {
        #region CONSTRUCTORS
        public LayerFX(Beat masterbeat, string layername, IMessenger messenger, ActionManager actionmanager)
            : this
            (
                actionmanager: actionmanager,
                feedback: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(LayerFX), "Feedback"), messenger, actionmanager),
                blur: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(LayerFX), "Blur"), messenger, actionmanager),
                messenger: messenger,
                messageaddress: String.Format("{0}/{1}/", layername, nameof(LayerFX)),
                messageEnabled: true
            )
        { }

        public LayerFX
            (
                IMessenger messenger,
                Slider feedback,
                Slider blur,
                string messageaddress,
                bool messageEnabled,
                ActionManager actionmanager
            )
            : base (actionmanager)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            Feedback = feedback;
            Blur = blur;
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
        }
        #endregion

        #region PROPERTIES
        private IMessenger Messenger { get; }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public BeatModifier BeatModifier { get; }

        public Slider Feedback { get; }

        public Slider Blur { get; }
        #endregion

        #region COPY/PASTE
        public void Copy(LayerFXDTO layerfxdto)
        {
            //layerfxdto.Feedback = Feedback;
            //layerfxdto.Blur = Blur;

        }

        public void Paste(LayerFXDTO layerfxdto)
        {
            MessageEnabled = false;

            //Feedback = layerfxdto.Feedback;
            //Blur = layerfxdto.Blur;

            MessageEnabled = true;
        }
        #endregion
    }
}
