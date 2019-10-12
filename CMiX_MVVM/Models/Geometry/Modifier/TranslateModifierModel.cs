using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class TranslateModifierModel : Model
    {
        public TranslateModifierModel()
        {
            TranslatePattern = new GeometryTranslateModel();
            X = new SliderModel();
            Y = new SliderModel();
            Z = new SliderModel();
            TranslateBeatModifier = new BeatModifierModel();
        }

        public TranslateModifierModel(string messageaddress) : this()
        {
            MessageAddress = messageaddress;
            TranslateBeatModifier = new BeatModifierModel(messageaddress);
        }

        public SliderModel X { get; set; }
        public SliderModel Y { get; set; }
        public SliderModel Z { get; set; }
        public GeometryTranslateModel TranslatePattern { get; set; }
        public BeatModifierModel TranslateBeatModifier { get; set; }
    }
}