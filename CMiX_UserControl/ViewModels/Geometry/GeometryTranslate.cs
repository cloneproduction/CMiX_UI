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
        public GeometryTranslate(string messageAddress, MessageService messageService, Mementor mementor) 
        {
            MessageAddress = $"{messageAddress}/";
            MessageService = messageService;
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
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            MessageService.Disable();
            Mode = default;
            MessageService.Enable();
        }

        public void CopyModel(GeometryTranslateModel geometryTranslateModel)
        {
            geometryTranslateModel.Mode = Mode;
        }

        public void SetViewModel(GeometryTranslateModel geometryTranslateModel)
        {
            MessageService.Disable();
            Mode = geometryTranslateModel.Mode;
            MessageService.Enable();
        }
        #endregion
    }
}