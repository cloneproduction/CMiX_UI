using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ColorSelectorModel
    {
        public ColorSelectorModel()
        {
            ColorPickerModel = new ColorPickerModel();
        }

        public ColorPickerModel ColorPickerModel { get; set; }
    }
}