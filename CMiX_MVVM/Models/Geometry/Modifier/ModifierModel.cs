using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class XYZModifierModel : Model, IModel
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
    }
}