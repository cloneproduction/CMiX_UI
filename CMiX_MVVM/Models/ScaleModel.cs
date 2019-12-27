using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ScaleModel : IModel
    {
        public ScaleModel()
        {
            ScaleX = new SliderModel();
            ScaleY = new SliderModel();
            ScaleZ = new SliderModel();
        }

        public ScaleModel(string messageaddress)
            : this()
        {
            MessageAddress = messageaddress;
            ScaleX = new SliderModel(messageaddress);
            ScaleY = new SliderModel(messageaddress);
            ScaleZ = new SliderModel(messageaddress);
        }

        public SliderModel ScaleX { get; set; }
        public SliderModel ScaleY { get; set; }
        public SliderModel ScaleZ { get; set; }
        public string MessageAddress { get; set; }
    }
}
