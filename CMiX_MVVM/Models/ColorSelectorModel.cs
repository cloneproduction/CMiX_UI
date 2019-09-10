using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ColorSelectorModel : Model
    {
        public ColorSelectorModel()
        {
            ColorPickerModel = new ColorPickerModel();
        }

        public ColorPickerModel ColorPickerModel { get; set; }
    }
}