using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class TextureModel : IModel
    {
        public TextureModel()
        {
            AssetPathSelectorModel = new AssetPathSelectorModel();
            InverterModel = new InverterModel();
            Brightness = new SliderModel();
            Contrast = new SliderModel();
            Hue = new SliderModel();
            Saturation = new SliderModel();
            Luminosity = new SliderModel();
            Keying = new SliderModel();
            Pan = new SliderModel();
            Tilt = new SliderModel();
            Scale = new SliderModel();
            Rotate = new SliderModel();
        }

        public bool Enabled { get; set; }

        public AssetPathSelectorModel AssetPathSelectorModel { get; set; }
        public InverterModel InverterModel { get; set; }
        public SliderModel Brightness { get; set; }
        public SliderModel Contrast { get; set; }

        public SliderModel Hue { get; set; }
        public SliderModel Saturation { get; set; }
        public SliderModel Luminosity { get; set; }
        public SliderModel Keying { get; set; }
        public SliderModel Pan { get; set; }
        public SliderModel Tilt { get; set; }
        public SliderModel Scale { get; set; }
        public SliderModel Rotate { get; set; }

    }
}