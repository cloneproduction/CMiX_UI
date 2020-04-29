﻿using System;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.Studio.ViewModels
{
    public class GeometryTranslate : ViewModel, IUndoable
    {
        #region CONSTRUCTORS
        public GeometryTranslate(string messageAddress, MessengerService messengerService, Mementor mementor) 
        {
            MessageAddress = $"{messageAddress}/";
            MessengerService = messengerService;
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

        public void CopyModel(GeometryTranslateModel geometryTranslateModel)
        {
            geometryTranslateModel.Mode = Mode;
        }

        public void SetViewModel(GeometryTranslateModel geometryTranslateModel)
        {
            MessengerService.Disable();
            Mode = geometryTranslateModel.Mode;
            MessengerService.Enable();
        }
        #endregion
    }
}