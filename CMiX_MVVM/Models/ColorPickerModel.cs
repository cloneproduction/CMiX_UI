using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ColorPickerModel : Model
    {
        public ColorPickerModel()
        {
            SelectedColor = "#FFFF00FF";
        }

        public string SelectedColor { get; set; }
    }
}
