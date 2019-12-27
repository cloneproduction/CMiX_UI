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
        public string MessageAddress { get ; set; }
    }
}