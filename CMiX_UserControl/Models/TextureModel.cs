using System;

namespace CMiX.Models
{
    [Serializable]
    public class TextureModel
    {
        public TextureModel()
        {
            FileSelector = new FileSelectorModel();
            Brightness = new SliderModel();
            Contrast = new SliderModel();
            Invert = new SliderModel();
            Hue = new SliderModel();
            Saturation = new SliderModel();
            Luminosity = new SliderModel();
            Keying = new SliderModel();
            Pan = new SliderModel();
            Tilt = new SliderModel();
            Scale = new SliderModel();
            Rotate = new SliderModel();
        }

        public string MessageAddress { get; set; }
        public FileSelectorModel FileSelector { get; set; }
        public SliderModel Brightness { get; set; }
        public SliderModel Contrast { get; set; }
        public SliderModel Invert { get; set; }
        public SliderModel Hue { get; set; }
        public SliderModel Saturation { get; set; }
        public SliderModel Luminosity { get; set; }
        public SliderModel Keying { get; set; }
        public SliderModel Pan { get; set; }
        public SliderModel Tilt { get; set; }
        public SliderModel Scale { get; set; }
        public SliderModel Rotate { get; set; }
        public string InvertMode { get; set; }
    }
}