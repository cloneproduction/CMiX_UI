using System;

using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System.Collections.ObjectModel;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class GeometryScale : ViewModel, IUndoable
    {
        #region CONSTRUCTORS
        public GeometryScale(string messageAddress, MessengerService messengerService, Mementor mementor) 
        {
            MessageAddress = $"{messageAddress}/";
            MessengerService = messengerService;
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
        public MessengerService MessengerService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            MessengerService.Disable();
            Mode = default;
            MessengerService.Enable();
        }

        public void Copy(GeometryScaleModel geometryScaleModel)
        {
            geometryScaleModel.Mode = Mode;
        }

        public void Paste(GeometryScaleModel geometryScaleModel)
        {
            MessengerService.Disable();

            Mode = geometryScaleModel.Mode;

            MessengerService.Enable();
        }
        #endregion
    }
}