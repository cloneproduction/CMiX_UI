using System;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.Studio.ViewModels
{
    public class GeometryTranslate : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public GeometryTranslate(string messageaddress, Sender sender, Mementor mementor) 
        {
            MessageAddress = String.Format("{0}/", messageaddress);
            Sender = sender;
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
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            Sender.Disable();
            Mode = default;
            Sender.Enable();
        }

        public void CopyModel(GeometryTranslateModel geometryTranslateModel)
        {
            geometryTranslateModel.Mode = Mode;
        }

        public void PasteModel(GeometryTranslateModel geometryTranslateModel)
        {
            Sender.Disable();
            Mode = geometryTranslateModel.Mode;
            Sender.Enable();
        }
        #endregion
    }
}