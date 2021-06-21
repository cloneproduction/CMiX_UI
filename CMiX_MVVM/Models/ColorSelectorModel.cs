using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class ColorSelectorModel : Model, IModel
    {
        public ColorSelectorModel()
        {
            ID = Guid.NewGuid();
            ColorPickerModel = new ColorPickerModel();
        }

        public ColorPickerModel ColorPickerModel { get; set; }
    }
}