using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class ScaleModifierModel : XYZModifierModel
    {
        public ScaleModifierModel()
        {
            Uniform = new AnimParameterModel();
        }

        public AnimParameterModel Uniform { get; set; }
    }
}