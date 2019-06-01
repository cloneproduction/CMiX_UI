using CMiX.Services;
using CMiX.ViewModels;
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
        public GeometryTranslateMode TranslateMode { get; set; }
    }
}
