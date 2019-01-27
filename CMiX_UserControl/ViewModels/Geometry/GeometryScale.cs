using System;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    public class GeometryScale : ViewModel
    {
        #region CONSTRUCTORS
        public GeometryScale(string layername, OSCMessenger messenger, ActionManager actionmanager)
        : this
        (
            actionmanager: actionmanager,
            messageaddress: layername + "/",
            messenger: messenger,
            scaleMode: default
        )
        { }

        public GeometryScale
            (
                ActionManager actionmanager,
                string messageaddress,
                OSCMessenger messenger,
                GeometryScaleMode scaleMode
            )
            : base(actionmanager, messenger)
        {
            MessageAddress = messageaddress;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }
        #endregion

        #region PROPERTIES
        public string MessageAddress { get; set; }

        private GeometryScaleMode _ScaleMode;
        [OSC]
        public GeometryScaleMode ScaleMode
        {
            get => _ScaleMode;
            set
            {
                SetAndNotify(ref _ScaleMode, value);
                Messenger.SendMessage(MessageAddress + nameof(ScaleMode), ScaleMode);
            }
        }
        #endregion

        #region COPY/PASTE
        public void Copy(GeometryScaleDTO geometryscaledto)
        {
            geometryscaledto.ScaleModeDTO = ScaleMode;
        }

        public void Paste(GeometryScaleDTO geometryscaledto)
        {
            Messenger.SendEnabled = false;
            ScaleMode = geometryscaledto.ScaleModeDTO;
            Messenger.SendEnabled = true;
        }
        #endregion
    }
}