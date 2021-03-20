using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ColorPickerModel : Model
    {
        public ColorPickerModel()
        {
            SelectedColor = "#ff00ff";
        }

        public string SelectedColor { get; set; }
    }
}
