using CMiX.Services;
using System;

namespace CMiX.Models
{
    [Serializable]
    public class GeometryModel
    {
        public GeometryModel()
        {
            FileSelector = new FileSelectorModel();
            GeometryTranslate = new GeometryTranslateModel();
            GeometryScale = new GeometryScaleModel();
            GeometryRotation = new GeometryRotationModel();
            GeometryFX = new GeometryFXModel();
            Translate = new SliderModel();
            Scale = new SliderModel();
            Rotation = new SliderModel();
            Counter = new CounterModel();
        }

        public string MessageAddress { get; set; }

        public FileSelectorModel FileSelector { get; set; }
        public SliderModel Translate { get; set; }
        public SliderModel Scale { get; set; }
        public SliderModel Rotation { get; set; }
        public CounterModel Counter { get; set; }
        public GeometryTranslateModel GeometryTranslate { get; set; }
        public GeometryScaleModel GeometryScale { get; set; }
        public GeometryRotationModel GeometryRotation { get; set; }
        public GeometryFXModel GeometryFX { get; set; }

        [OSC]
        public bool Is3D { get; set; }

        [OSC]
        public bool KeepAspectRatio { get; set; }
    }
}