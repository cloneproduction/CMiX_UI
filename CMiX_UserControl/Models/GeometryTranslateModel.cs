using CMiX.Services;
using CMiX.ViewModels;
using System;

namespace CMiX.Models
{
    [Serializable]
    public class GeometryTranslateModel
    {
        public string MessageAddress { get; set; }

        [OSC]
        public GeometryTranslateMode TranslateMode { get; set; }
    }
}
