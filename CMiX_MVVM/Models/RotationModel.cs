using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class RotationModel : IModel
    {
        public RotationModel()
        {
            RotationX = new SliderModel();
            RotationY = new SliderModel();
            RotationZ = new SliderModel();
        }

        public RotationModel(string messageaddress)
            : this()
        {
            MessageAddress = messageaddress;
            RotationX = new SliderModel(messageaddress);
            RotationY = new SliderModel(messageaddress);
            RotationZ = new SliderModel(messageaddress);
        }

        public SliderModel RotationX { get; set; }
        public SliderModel RotationY { get; set; }
        public SliderModel RotationZ { get; set; }
        public string MessageAddress { get; set; }
    }
}
