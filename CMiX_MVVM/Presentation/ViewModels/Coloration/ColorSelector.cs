using CMiX.Core.Interfaces;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Models;
using System;
using System.Windows.Media;

namespace CMiX.Core.Presentation.ViewModels
{
    public class ColorSelector : ViewModel, IControl
    {
        public ColorSelector(ColorSelectorModel colorSelectorModel)
        {
            this.ID = colorSelectorModel.ID;
            ColorPicker = new ColorPicker(colorSelectorModel.ColorPickerModel);
            //this.SelectedColor = Utils.HexStringToColor(colorSelectorModel.ColorPickerModel.SelectedColor);
        }

        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);

            ColorPicker.SetCommunicator(Communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);

            ColorPicker.UnsetCommunicator(Communicator);
        }

        public Guid ID { get; set; }
        public ControlCommunicator Communicator { get; set; }
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

        public void SetViewModel(IModel model)
        {
            ColorSelectorModel colorSelectorModel = model as ColorSelectorModel;
            this.ID = colorSelectorModel.ID;
            this.ColorPicker.SetViewModel(colorSelectorModel.ColorPickerModel);
        }

        public IModel GetModel()
        {
            ColorSelectorModel model = new ColorSelectorModel();
            model.ID = this.ID;
            model.ColorPickerModel = (ColorPickerModel)ColorPicker.GetModel();
            return model;
        }
    }
}
