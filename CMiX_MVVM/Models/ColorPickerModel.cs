using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ColorPickerModel : Model
    {
        public ColorPickerModel()
        {

        }

        public string SelectedColor { get; set; }
    }
}
