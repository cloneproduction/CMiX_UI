using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ScaleModifierModel : Model
    {
        public ScaleModifierModel()
        {
            ScalePattern = new GeometryScaleModel();
            X = new SliderModel();
            Y = new SliderModel();
            Z = new SliderModel();
            ScaleBeatModifier = new BeatModifierModel();
        }

        public ScaleModifierModel(string messageaddress) : this()
        {
            MessageAddress = messageaddress;
            ScaleBeatModifier = new BeatModifierModel(messageaddress);
        }

        public SliderModel X { get; set; }
        public SliderModel Y { get; set; }
        public SliderModel Z { get; set; }
        public GeometryScaleModel ScalePattern { get; set; }
        public BeatModifierModel ScaleBeatModifier { get; set; }
    }
}