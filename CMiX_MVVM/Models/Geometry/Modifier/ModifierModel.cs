using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ModifierModel : Model
    {
        public ModifierModel()
        {
            Counter = new CounterModel();
            TranslateModifier = new TranslateModifierModel();
            ScaleModifier = new ScaleModifierModel();
            RotationModifier = new RotationModifierModel();
        }

        public ModifierModel(string messageaddress) : this()
        {
            MessageAddress = messageaddress;

            Counter = new CounterModel(messageaddress);
            TranslateModifier = new TranslateModifierModel(messageaddress);
            ScaleModifier = new ScaleModifierModel(messageaddress);
            RotationModifier = new RotationModifierModel(messageaddress);
        }

        public CounterModel Counter { get; set; }
        public TranslateModifierModel TranslateModifier { get; set; }
        public ScaleModifierModel ScaleModifier { get; set; }
        public RotationModifierModel RotationModifier { get; set; }
        public bool NoAspectRatio { get; set; }
    }
}