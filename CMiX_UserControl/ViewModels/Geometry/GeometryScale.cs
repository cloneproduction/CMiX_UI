using System;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;
using System.Collections.ObjectModel;

namespace CMiX.ViewModels
{
    public class GeometryScale : ViewModel
    {
        #region CONSTRUCTORS
        public GeometryScale(string layername, ObservableCollection<OSCMessenger> messengers, ActionManager actionmanager)
        : this
        (
            actionmanager: actionmanager,
            messageaddress: layername + "/",
            messengers: messengers,
            scaleMode: default
        )
        { }

        public GeometryScale
            (
                ActionManager actionmanager,
                string messageaddress,
                ObservableCollection<OSCMessenger> messengers,
                GeometryScaleMode scaleMode
            )
            : base(actionmanager, messengers)
        {
            MessageAddress = messageaddress;
            Messengers = messengers ?? throw new ArgumentNullException(nameof(messengers));
        }
        #endregion

        #region PROPERTIES
        private GeometryScaleMode _ScaleMode;
        [OSC]
        public GeometryScaleMode ScaleMode
        {
            get => _ScaleMode;
            set
            {
                SetAndNotify(ref _ScaleMode, value);
                SendMessages(MessageAddress + nameof(ScaleMode), ScaleMode);
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
            DisabledMessages();
            ScaleMode = geometryscaledto.ScaleModeDTO;
            EnabledMessages();
        }
        #endregion
    }
}