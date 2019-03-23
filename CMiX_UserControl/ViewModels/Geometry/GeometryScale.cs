using System;
using CMiX.Services;
using CMiX.Models;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class GeometryScale : ViewModel
    {
        #region CONSTRUCTORS
        public GeometryScale(string layername, ObservableCollection<OSCMessenger> messengers, Mementor mementor)
        : this
        (
            mementor: mementor,
            messageaddress: layername + "/",
            messengers: messengers,
            scaleMode: default
        )
        { }

        public GeometryScale
            (
                Mementor mementor,
                string messageaddress,
                ObservableCollection<OSCMessenger> messengers,
                GeometryScaleMode scaleMode
            )
            : base(messengers)
        {
            Mementor = mementor;
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
                Mementor.PropertyChange(this, "ScaleMode");
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