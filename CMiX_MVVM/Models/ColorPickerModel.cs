using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ColorPickerModel : IModel
    {
        public ColorPickerModel()
        {
            SelectedColor = "#FFFF00FF";
        }

        public string SelectedColor { get; set; }
        public string MessageAddress { get; set; }
    }
}
