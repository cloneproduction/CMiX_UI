using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class TranslateModifierModel : IModel
    {
        public TranslateModifierModel()
        {
            Translate = new AnimParameterModel();
            TranslateX = new AnimParameterModel();
            TranslateY = new AnimParameterModel();
            TranslateZ = new AnimParameterModel();
        }

        public TranslateModifierModel(string messageaddress) : this()
        {
            MessageAddress = messageaddress;
            TranslateX = new AnimParameterModel(messageaddress);
            TranslateY = new AnimParameterModel(messageaddress);
            TranslateZ = new AnimParameterModel(messageaddress);
        }

        public AnimParameterModel Translate { get; set; }
        public AnimParameterModel TranslateX { get; set; }
        public AnimParameterModel TranslateY { get; set; }
        public AnimParameterModel TranslateZ { get; set; }
        public string MessageAddress { get; set; }
    }
}