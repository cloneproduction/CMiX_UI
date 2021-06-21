using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class InstancerModel : Model, IModel
    {
        public InstancerModel()
        {
            ID = Guid.NewGuid();
            Transform = new TransformModel();
            Counter = new CounterModel();
            TranslateModifierModel = new XYZModifierModel();
            ScaleModifierModel = new XYZModifierModel();
            RotationModifierModel = new XYZModifierModel();
            UniformScale = new AnimParameterModel();
        }

        public AnimParameterModel UniformScale { get; set; }
        public TransformModel Transform { get; set; }
        public CounterModel Counter { get; set; }
        public XYZModifierModel TranslateModifierModel { get; set; }
        public XYZModifierModel ScaleModifierModel { get; set; }
        public XYZModifierModel RotationModifierModel { get; set; }
        public bool NoAspectRatio { get; set; }
    }
}