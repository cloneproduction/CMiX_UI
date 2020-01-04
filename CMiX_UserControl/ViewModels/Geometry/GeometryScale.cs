using System;

using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System.Collections.ObjectModel;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class GeometryScale : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public GeometryScale(string messageaddress, Messenger messenger, Mementor mementor) 
        {
            MessageAddress = String.Format("{0}/", messageaddress);
            Mode = default;
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
        }
        #endregion

        #region PROPERTIES
        private GeometryScaleMode _Mode;
        public GeometryScaleMode Mode
        {
            get => _Mode;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, nameof(Mode));
                SetAndNotify(ref _Mode, value);
                //SendMessages(MessageAddress + nameof(Mode), Mode);
            }
        }

        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            Messenger.Disable();
            Mode = default;
            Messenger.Enable();
        }

        public void Copy(GeometryScaleModel geometryscalemodel)
        {
            geometryscalemodel.MessageAddress = MessageAddress;
            geometryscalemodel.Mode = Mode;
        }

        public void Paste(GeometryScaleModel geometryscalemodel)
        {
            Messenger.Disable();

            MessageAddress = geometryscalemodel.MessageAddress;
            Mode = geometryscalemodel.Mode;

            Messenger.Enable();
        }
        #endregion
    }
}