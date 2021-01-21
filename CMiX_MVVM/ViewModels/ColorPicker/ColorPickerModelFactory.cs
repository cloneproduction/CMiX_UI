using CMiX.MVVM.Models;
using CMiX.MVVM.Tools;

namespace CMiX.MVVM.ViewModels
{
    public static class ColorPickerModelFactory
    {
        public static ColorPickerModel GetModel(this ColorPicker instance)
        {
            ColorPickerModel model = new ColorPickerModel();
            model.SelectedColor = Utils.ColorToHexString(instance.SelectedColor);
            return model;
        }

        public static void SetViewModel(this ColorPicker instance, ColorPickerModel model)
        {
            instance.SelectedColor = Utils.HexStringToColor(model.SelectedColor);
        }
    }
}
