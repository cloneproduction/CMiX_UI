using CMiX.Models;
using CMiX.Services;
using System;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    public class LayerFX : ViewModel
    {
        #region CONSTRUCTORS
        public LayerFX(Beat masterbeat, string layername, OSCMessenger messenger, ActionManager actionmanager)
            : this
            (
                actionmanager: actionmanager,
                feedback: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(LayerFX), "Feedback"), messenger, actionmanager),
                blur: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(LayerFX), "Blur"), messenger, actionmanager),
                messenger: messenger,
                messageaddress: String.Format("{0}/{1}/", layername, nameof(LayerFX))
            )
        { }

        public LayerFX
            (
                OSCMessenger messenger,
                Slider feedback,
                Slider blur,
                string messageaddress,
                ActionManager actionmanager
            )
            : base (actionmanager)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            Feedback = feedback;
            Blur = blur;
            MessageAddress = messageaddress;
        }
        #endregion

        #region PROPERTIES
        public string MessageAddress { get; set; }
        public BeatModifier BeatModifier { get; }
        public Slider Feedback { get; }
        public Slider Blur { get; }
        #endregion

        #region COPY/PASTE
        public void Copy(LayerFXDTO layerfxdto)
        {
            Feedback.Copy(layerfxdto.Feedback);
            Blur.Copy(layerfxdto.Blur);
        }

        public void Paste(LayerFXDTO layerfxdto)
        {

            Messenger.SendEnabled = false;

            Feedback.Paste(layerfxdto.Feedback);
            Blur.Paste(layerfxdto.Blur);

            Messenger.SendEnabled = true;
        }
        #endregion
    }
}
