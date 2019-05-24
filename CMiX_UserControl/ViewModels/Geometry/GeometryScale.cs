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
        public GeometryScale(string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) : base (oscmessengers, mementor)
        {
            MessageAddress = messageaddress;
            ScaleMode = default;
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
        }
        #endregion

        #region PROPERTIES
        private GeometryScaleMode _ScaleMode;
        public GeometryScaleMode ScaleMode
        {
            get => _ScaleMode;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, nameof(ScaleMode));
                SetAndNotify(ref _ScaleMode, value);
                SendMessages(MessageAddress + nameof(ScaleMode), ScaleMode);
            }
        }
        #endregion

        #region COPY/PASTE
        public void Copy(GeometryScaleModel geometryscalemodel)
        {
            geometryscalemodel.MessageAddress = MessageAddress;
            geometryscalemodel.ScaleMode = ScaleMode;
        }

        public void Paste(GeometryScaleModel geometryscalemodel)
        {
            DisabledMessages();
            MessageAddress = geometryscalemodel.MessageAddress;
            ScaleMode = geometryscalemodel.ScaleMode;
            EnabledMessages();
        }
        #endregion
    }
}