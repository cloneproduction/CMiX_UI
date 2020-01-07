using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class TranslateModifierModel
    {
        public TranslateModifierModel()
        {
            Translate = new AnimParameterModel();
            TranslateX = new AnimParameterModel();
            TranslateY = new AnimParameterModel();
            TranslateZ = new AnimParameterModel();
        }

        public AnimParameterModel Translate { get; set; }
        public AnimParameterModel TranslateX { get; set; }
        public AnimParameterModel TranslateY { get; set; }
        public AnimParameterModel TranslateZ { get; set; }
    }
}