using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class ColorSelectorModelFactory
    {
        public static ColorSelectorModel GetModel(this ColorSelector instance)
        {
            ColorSelectorModel colorSelectorModel = new ColorSelectorModel();
            colorSelectorModel.ColorPickerModel = instance.ColorPicker.GetModel();
            return colorSelectorModel;
        }

        public static void SetViewModel(this ColorSelector instance, ColorSelectorModel colorSelectorModel)
        {
            instance.ColorPicker.SetViewModel(colorSelectorModel.ColorPickerModel);
        }
    }
}