using System;
using CMiX.Services;
using CMiX.Models;

namespace CMiX.ViewModels
{
    public class GeometryScale : ViewModel, IMessengerData
    {
        public GeometryScale(string layername, IMessenger messenger)
            : this(
                  messageaddress: layername + "/",
                  messenger: messenger,
                  messageEnabled : true,
                  scaleMode: default
                  )
        {
        }

        public GeometryScale
            (
                string messageaddress,
                bool messageEnabled,
                IMessenger messenger,
                GeometryScaleMode scaleMode
            )
        {
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public IMessenger Messenger { get; }

        private GeometryScaleMode _ScaleMode;
        [OSC]
        public GeometryScaleMode ScaleMode
        {
            get => _ScaleMode;
            set
            {
                SetAndNotify(ref _ScaleMode, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(ScaleMode), ScaleMode);
            }
        }

        public void Copy(GeometryScaleDTO geometryscaledto)
        {
            geometryscaledto.ScaleModeDTO = ScaleMode;
        }

        public void Paste(GeometryScaleDTO geometryscaledto)
        {
            MessageEnabled = false;

            ScaleMode = geometryscaledto.ScaleModeDTO;

            MessageEnabled = true;
        }
    }
}