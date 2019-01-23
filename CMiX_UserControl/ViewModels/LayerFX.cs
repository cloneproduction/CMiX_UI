using CMiX.Models;
using CMiX.Services;
using System;

namespace CMiX.ViewModels
{
    public class LayerFX : ViewModel, IMessengerData
    {
        #region CONSTRUCTORS
        public LayerFX(Beat masterbeat, string layername, IMessenger messenger)
            : this(
                  feedback: 0.0,
                  blur: 0.0,
                  messenger: messenger,
                  messageaddress: String.Format("{0}/{1}/", layername, nameof(LayerFX)),
                  messageEnabled: true
                  )
        { }

        public LayerFX(
            IMessenger messenger,
            double feedback,
            double blur,
            string messageaddress,
            bool messageEnabled)
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

        private double _feedback;
        [OSC]
        public double Feedback
        {
            get => _feedback;
            set
            {
                SetAndNotify(ref _feedback, CoerceNotNegative(value));
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Feedback), Feedback);
            }
        }

        private double _blur;
        [OSC]
        public double Blur
        {
            get => _blur;
            set
            {
                SetAndNotify(ref _blur, CoerceNotNegative(value));
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Blur), Blur);
            }
        }
        #endregion

        #region COPY/PASTE
        public void Copy(LayerFXDTO layerfxdto)
        {
            layerfxdto.Feedback = Feedback;
            layerfxdto.Blur = Blur;

        }

        public void Paste(LayerFXDTO layerfxdto)
        {
            MessageEnabled = false;

            Feedback = layerfxdto.Feedback;
            Blur = layerfxdto.Blur;

            MessageEnabled = true;
        }
        #endregion
    }
}
