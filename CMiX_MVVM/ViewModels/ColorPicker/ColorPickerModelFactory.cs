using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;

namespace CMiX.MVVM.ViewModels
{
    public static class ColorPickerModelFactory
    {
        public static ColorPickerModel GetModel(this ColorPicker instance)
        {
            ColorPickerModel model = new ColorPickerModel();
            model.SelectedColor = Utils.ColorToHexString(instance.SelectedColor);
            System.Console.WriteLine("ColorPickerModel GetModel " + model.SelectedColor);
            return model;
        }

        public static void SetViewModel(this ColorPicker instance, ColorPickerModel model)
        {
            instance.SelectedColor = Utils.HexStringToColor(model.SelectedColor);
        }
    }
}
