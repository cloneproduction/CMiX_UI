using CMiX.Services;
using CMiX.ViewModels;
using CMiX.MVVM.Models;
using System;

namespace CMiX.Models
{
    [Serializable]
    public class GeometryTranslateModel : Model
    {
        public GeometryTranslateModel()
        {

        }

        [OSC]
        public GeometryTranslateMode Mode { get; set; }
    }
}
