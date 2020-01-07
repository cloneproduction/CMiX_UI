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
            RotationX = new SliderModel();
            RotationY = new SliderModel();
            RotationZ = new SliderModel();
        }

        public SliderModel RotationX { get; set; }
        public SliderModel RotationY { get; set; }
        public SliderModel RotationZ { get; set; }
    }
}
