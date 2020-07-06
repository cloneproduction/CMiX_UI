using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class XYZModifierModel
    {
        public XYZModifierModel()
        {
            Uniform = new AnimParameterModel();
            X = new AnimParameterModel();
            Y = new AnimParameterModel();
            Z = new AnimParameterModel();
        }

        public string Name { get; set; }
        public AnimParameterModel Uniform { get; set; }
        public AnimParameterModel X { get; set; }
        public AnimParameterModel Y { get; set; }
        public AnimParameterModel Z { get; set; }
    }
}