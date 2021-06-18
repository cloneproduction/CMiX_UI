using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models
{
    public class RotationModel : Model, IModel
    {
        public RotationModel()
        {
            this.ID = Guid.NewGuid();
            X = new SliderModel();
            Y = new SliderModel();
            Z = new SliderModel();
        }

        public SliderModel X { get; set; }
        public SliderModel Y { get; set; }
        public SliderModel Z { get; set; }
    }
}
