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
            TexturePaths = new List<ListBoxFileName>();
        }

        public string MessageAddress { get; set; }

        public List<ListBoxFileName> TexturePaths { get; set; }

        public double Brightness { get; set; }

        public double Contrast { get; set; }

        public double Hue { get; set; }

        public double Saturation { get; set; }

        public double Luminosity { get; set; }

        public double Pan { get; set; }

        public double Tilt { get; set; }

        public double Scale { get; set; }

        public double Rotate { get; set; }

        public double Keying { get; set; }

        public double Invert { get; set; }

        public string InvertMode { get; set; }
    }
}