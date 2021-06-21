// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using Memento;
using CMiX.Core.Presentation.ViewModels;
using CMiX.Core.Models;
using CMiX.Core.Services;
using CMiX.Core;

namespace CMiX.Core.Presentation.ViewModels
{
    public class GeometryTranslate : ViewModel
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