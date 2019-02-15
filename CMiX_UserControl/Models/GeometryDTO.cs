using System;

namespace CMiX.Models
{
    [Serializable]
    public class GeometryDTO
    {
        public GeometryDTO()
        {
            GeometryTranslate = new GeometryTranslateDTO();
            GeometryScale = new GeometryScaleDTO();
            GeometryRotation = new GeometryRotationDTO();
            FileSelector = new FileSelectorDTO();
            GeometryFX = new GeometryFXDTO();
            TranslateAmount = new SliderDTO();
            ScaleAmount = new SliderDTO();
            RotationAmount = new SliderDTO();
        }

        public string MessageAddress { get; set; }
        public FileSelectorDTO FileSelector { get; set; }
        public SliderDTO TranslateAmount { get; set; }
        public SliderDTO ScaleAmount { get; set; }
        public SliderDTO RotationAmount { get; set; }
        public bool Is3D { get; set; }
        public bool KeepAspectRatio { get; set; }
        public CounterDTO Count { get; set; }
        public GeometryTranslateDTO GeometryTranslate { get; set; }
        public GeometryScaleDTO GeometryScale { get; set; }
        public GeometryRotationDTO GeometryRotation { get; set; }
        public GeometryFXDTO GeometryFX { get; set; }
    }
}