using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models
{
    public class TranslateModel : Model, IModel
    {
        public TranslateModel()
        {
            this.ID = Guid.NewGuid();
            X = new SliderModel();
            Y = new SliderModel();
            Z = new SliderModel();
        }

        public SliderModel X { get; set; }
        public SliderModel Y { get; set; }
        public SliderModel Z { get; set; }
    }
}