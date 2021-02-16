using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class XYZModifierModel : IModel
    {
        public XYZModifierModel()
        {
            X = new AnimParameterModel();
            Y = new AnimParameterModel();
            Z = new AnimParameterModel();
        }

        public string Name { get; set; }
        public AnimParameterModel X { get; set; }
        public AnimParameterModel Y { get; set; }
        public AnimParameterModel Z { get; set; }
        public bool Enabled { get; set; }
    }
}