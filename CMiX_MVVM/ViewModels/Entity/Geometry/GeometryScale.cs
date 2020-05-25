using System;

using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System.Collections.ObjectModel;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class GeometryScale : ViewModel
    {
        #region CONSTRUCTORS
        public GeometryScale() 
        {
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

        public void Copy(GeometryScaleModel geometryScaleModel)
        {
            geometryScaleModel.Mode = Mode;
        }

        public void Paste(GeometryScaleModel geometryScaleModel)
        {
            Mode = geometryScaleModel.Mode;
        }
        #endregion
    }
}