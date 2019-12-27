using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class TranslateModel : IModel
    {
        public TranslateModel()
        {
            TranslateX = new SliderModel();
            TranslateY = new SliderModel();
            TranslateZ = new SliderModel();
        }

        public TranslateModel(string messageaddress)
            : this()
        {
            MessageAddress = messageaddress;
            TranslateX = new SliderModel(messageaddress);
            TranslateY = new SliderModel(messageaddress);
            TranslateZ = new SliderModel(messageaddress);
        }

        public SliderModel TranslateX { get; set; }
        public SliderModel TranslateY { get; set; }
        public SliderModel TranslateZ { get; set; }
        public string MessageAddress { get; set; }
    }
}