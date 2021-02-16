using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ColorSelectorModel : IModel
    {
        public ColorSelectorModel()
        {
            ColorPickerModel = new ColorPickerModel();
        }

        public ColorPickerModel ColorPickerModel { get; set; }
        public bool Enabled { get; set; }
    }
}