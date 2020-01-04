using System;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.Studio.ViewModels
{
    public class GeometryTranslate : ViewModel, ISendable, ICopyPasteModel, IUndoable
    {
        #region CONSTRUCTORS
        public GeometryTranslate(string messageaddress, Messenger messenger, Mementor mementor) 
        {
            MessageAddress = String.Format("{0}/", messageaddress);
            Messenger = messenger;
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
        }
        #endregion

        #region PROPERTIES
        private GeometryTranslateMode _Mode;
        public GeometryTranslateMode Mode
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

        public void CopyModel(IModel model)
        {
            GeometryTranslateModel geometryTranslateModel = model as GeometryTranslateModel;
            geometryTranslateModel.MessageAddress = MessageAddress;
            geometryTranslateModel.Mode = Mode;
        }

        public void PasteModel(IModel model)
        {
            GeometryTranslateModel geometryTranslateModel = model as GeometryTranslateModel;
            Messenger.Disable();
            MessageAddress = geometryTranslateModel.MessageAddress;
            Mode = geometryTranslateModel.Mode;
            Messenger.Enable();
        }
        #endregion
    }
}