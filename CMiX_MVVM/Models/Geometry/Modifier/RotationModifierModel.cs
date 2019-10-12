using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class RotationModifierModel : Model
    {
        public RotationModifierModel()
        {
            RotationPattern = new GeometryRotationModel();
            X = new SliderModel();
            Y = new SliderModel();
            Z = new SliderModel();
            RotationBeatModifier = new BeatModifierModel();
        }

        public RotationModifierModel(string messageaddress) : this()
        {
            MessageAddress = messageaddress;
            RotationBeatModifier = new BeatModifierModel(messageaddress);
        }

        public SliderModel X { get; set; }
        public SliderModel Y { get; set; }
        public SliderModel Z { get; set; }
        public GeometryRotationModel RotationPattern { get; set; }
        public BeatModifierModel RotationBeatModifier { get; set; }
    }
}