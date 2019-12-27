using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class AnimParameterModel : IModel
    {
        public AnimParameterModel()
        {
            Slider = new SliderModel();
            BeatModifier = new BeatModifierModel();
        }

        public AnimParameterModel(string messageaddress) : this()
        {
            Slider = new SliderModel(messageaddress);
            BeatModifier = new BeatModifierModel(messageaddress);
        }

        public SliderModel Slider { get; set; }
        public BeatModifierModel BeatModifier { get; set; }
        public string MessageAddress { get; set; }
    }
}