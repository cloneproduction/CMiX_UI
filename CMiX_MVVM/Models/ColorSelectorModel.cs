using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models
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