using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class RotationModifierModel : IModel
    {
        public RotationModifierModel()
        {
            Rotation = new AnimParameterModel();
            RotationX = new AnimParameterModel();
            RotationY = new AnimParameterModel();
            RotationZ = new AnimParameterModel();
        }

        public RotationModifierModel(string messageaddress) : this()
        {
            MessageAddress = messageaddress;
            RotationX = new AnimParameterModel(messageaddress);
            RotationY = new AnimParameterModel(messageaddress);
            RotationZ = new AnimParameterModel(messageaddress);
        }

        public AnimParameterModel Rotation { get; set; }
        public AnimParameterModel RotationX { get; set; }
        public AnimParameterModel RotationY { get; set; }
        public AnimParameterModel RotationZ { get; set; }
        public string MessageAddress { get; set; }
    }
}