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
            TranslateAmount = new SliderModel();
            ScaleAmount = new SliderModel();
            RotationAmount = new SliderModel();
        }

        public string MessageAddress { get; set; }
        public FileSelectorModel FileSelector { get; set; }
        public SliderModel TranslateAmount { get; set; }
        public SliderModel ScaleAmount { get; set; }
        public SliderModel RotationAmount { get; set; }
        public CounterModel Count { get; set; }
        public GeometryTranslateModel GeometryTranslate { get; set; }
        public GeometryScaleModel GeometryScale { get; set; }
        public GeometryRotationModel GeometryRotation { get; set; }
        public GeometryFXModel GeometryFX { get; set; }
        public bool Is3D { get; set; }
        public bool KeepAspectRatio { get; set; }
    }
}