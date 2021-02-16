using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ScaleModel : IModel
    {
        public ScaleModel()
        {
            X = new SliderModel();
            Y = new SliderModel();
            Z = new SliderModel();
            Uniform = new SliderModel();
        }

        public SliderModel X { get; set; }
        public SliderModel Y { get; set; }
        public SliderModel Z { get; set; }
        public SliderModel Uniform { get; set; }
        public bool Enabled { get; set; }
    }
}
