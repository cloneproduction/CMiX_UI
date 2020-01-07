using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class InstancerModel
    {
        public InstancerModel()
        {
            Transform = new TransformModel();
            Counter = new CounterModel();
            TranslateModifier = new TranslateModifierModel();
            ScaleModifier = new ScaleModifierModel();
            RotationModifier = new RotationModifierModel();
        }

        public TransformModel Transform { get; set; }
        public CounterModel Counter { get; set; }
        public TranslateModifierModel TranslateModifier { get; set; }
        public ScaleModifierModel ScaleModifier { get; set; }
        public RotationModifierModel RotationModifier { get; set; }
        public bool NoAspectRatio { get; set; }
    }
}