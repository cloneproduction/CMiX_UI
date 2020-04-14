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
        public GeometryScale(string messageAddress, MessageService messageService, Mementor mementor) 
        {
            MessageAddress = $"{messageAddress}/";
            MessageService = messageService;
            Mode = default;
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

        public void Copy(GeometryScaleModel geometryScaleModel)
        {
            geometryScaleModel.Mode = Mode;
        }

        public void Paste(GeometryScaleModel geometryScaleModel)
        {
            MessageService.Disable();

            Mode = geometryScaleModel.Mode;

            MessageService.Enable();
        }
        #endregion
    }
}