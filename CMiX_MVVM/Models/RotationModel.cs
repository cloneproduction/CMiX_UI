using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class RotationModel
    {
        public RotationModel()
        {
            X = new SliderModel();
            Y = new SliderModel();
            Z = new SliderModel();
        }

        public SliderModel X { get; set; }
        public SliderModel Y { get; set; }
        public SliderModel Z { get; set; }
    }
}
