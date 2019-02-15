using System;
using CMiX.Controls;
using System.Collections.Generic;

namespace CMiX.Models
{
    [Serializable]
    public class TextureDTO
    {
        public TextureDTO()
        {
            FileSelector = new FileSelectorDTO();
            Brightness = new SliderDTO();
            Contrast = new SliderDTO();
            Invert = new SliderDTO();
            Hue = new SliderDTO();
            Saturation = new SliderDTO();
            Luminosity = new SliderDTO();
            Keying = new SliderDTO();
            Pan = new SliderDTO();
            Tilt = new SliderDTO();
            Scale = new SliderDTO();
            Rotate = new SliderDTO();
        }

        public string MessageAddress { get; set; }
        public FileSelectorDTO FileSelector { get; set; }
        public SliderDTO Brightness { get; set; }
        public SliderDTO Contrast { get; set; }
        public SliderDTO Invert { get; set; }
        public SliderDTO Hue { get; set; }
        public SliderDTO Saturation { get; set; }
        public SliderDTO Luminosity { get; set; }
        public SliderDTO Keying { get; set; }
        public SliderDTO Pan { get; set; }
        public SliderDTO Tilt { get; set; }
        public SliderDTO Scale { get; set; }
        public SliderDTO Rotate { get; set; }
        public string InvertMode { get; set; }
    }
}