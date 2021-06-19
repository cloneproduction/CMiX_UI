using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class ColorPickerModel : Model
    {
        public ColorPickerModel()
        {
            ID = Guid.NewGuid();
            SelectedColor = "#ff00ff";
        }

        public string SelectedColor { get; set; }
    }
}
