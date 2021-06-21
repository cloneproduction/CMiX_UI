using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class ScaleModel : Model, IModel
    {
        public ScaleModel()
        {
            ID = Guid.NewGuid();
            X = new SliderModel();
            Y = new SliderModel();
            Z = new SliderModel();
            Uniform = new SliderModel();
        }

        public SliderModel X { get; set; }
        public SliderModel Y { get; set; }
        public SliderModel Z { get; set; }
        public SliderModel Uniform { get; set; }
    }
}
