using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using System.Windows.Media;

namespace CMiX.MVVM.ViewModels
{
    public class ColorSelector : Control
    {
        public ColorSelector(ColorSelectorModel colorSelectorModel)
        {
            this.ID = colorSelectorModel.ID;
            ColorPicker = new ColorPicker(colorSelectorModel.ColorPickerModel);
            //this.SelectedColor = Utils.HexStringToColor(colorSelectorModel.ColorPickerModel.SelectedColor);
        }

        public override void SetCommunicator(ICommunicator communicator)
        {
            base.SetCommunicator(communicator);

            ColorPicker.SetCommunicator(Communicator);
        }


        public ColorPicker ColorPicker { get; set; }

        private Color _selectedColor;
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                SetAndNotify(ref _selectedColor, value);
            }
        }

        public override void SetViewModel(IModel model)
        {
            ColorSelectorModel colorSelectorModel = model as ColorSelectorModel;
            this.ID = colorSelectorModel.ID;
            this.ColorPicker.SetViewModel(colorSelectorModel.ColorPickerModel);
        }

        public override IModel GetModel()
        {
            ColorSelectorModel model = new ColorSelectorModel();
            model.ID = this.ID;
            model.ColorPickerModel = (ColorPickerModel)ColorPicker.GetModel();
            return model;
        }
    }
}
