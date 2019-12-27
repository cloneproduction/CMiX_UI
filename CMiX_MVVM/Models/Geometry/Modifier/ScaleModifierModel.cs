using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ScaleModifierModel : IModel
    {
        public ScaleModifierModel()
        {
            Scale = new AnimParameterModel();
            ScaleX = new AnimParameterModel();
            ScaleY = new AnimParameterModel();
            ScaleZ = new AnimParameterModel();
        }

        public ScaleModifierModel(string messageaddress) : this()
        {
            MessageAddress = messageaddress;
            ScaleX = new AnimParameterModel(messageaddress);
            ScaleY = new AnimParameterModel(messageaddress);
            ScaleZ = new AnimParameterModel(messageaddress);
        }

        public AnimParameterModel Scale { get; set; }
        public AnimParameterModel ScaleX { get; set; }
        public AnimParameterModel ScaleY { get; set; }
        public AnimParameterModel ScaleZ { get; set; }
        public string MessageAddress { get; set; }
    }
}