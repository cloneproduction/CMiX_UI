using System;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.MVVM.ViewModels
{
    public class GeometryTranslate : Sendable
    {
        #region CONSTRUCTORS
        public GeometryTranslate(string messageAddress, Mementor mementor) 
        {

        }
        #endregion

        #region PROPERTIES
        private GeometryTranslateMode _Mode;
        public GeometryTranslateMode Mode
        {
            get => _Mode;
            set
            {
                //if(Mementor != null)
                //    Mementor.PropertyChange(this, nameof(Mode));
                SetAndNotify(ref _Mode, value);
                //SendMessages(MessageAddress + nameof(Mode), Mode);
            }
        }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            Mode = default;
        }

        public void CopyModel(GeometryTranslateModel geometryTranslateModel)
        {
            geometryTranslateModel.Mode = Mode;
        }

        public void SetViewModel(GeometryTranslateModel geometryTranslateModel)
        {
            Mode = geometryTranslateModel.Mode;
        }
        #endregion
    }
}