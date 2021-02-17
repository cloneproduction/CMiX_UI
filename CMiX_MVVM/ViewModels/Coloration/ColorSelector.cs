using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System.Windows.Media;

namespace CMiX.MVVM.ViewModels
{
    public class ColorSelector : Sender
    {
        public ColorSelector(string name, IMessageProcessor parentSender) : base(name, parentSender)
        {
            ColorPicker = new ColorPicker(nameof(ColorPicker), this);
            this.SelectedColor = Colors.Red;
        }

        public ColorPicker ColorPicker { get; set; }

        private Color _selectedColor;
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                SetAndNotify(ref _selectedColor, value);
                this.MessageDispatcher?.NotifyOut(new MessageUpdateViewModel(this.GetAddress(), this.GetModel()));
            }
        }

        public override void SetViewModel(IModel model)
        {
            ColorSelectorModel colorSelectorModel = model as ColorSelectorModel;
            this.ColorPicker.SetViewModel(colorSelectorModel.ColorPickerModel);
        }

        public override IModel GetModel()
        {
            ColorSelectorModel colorSelectorModel = new ColorSelectorModel();
            colorSelectorModel.ColorPickerModel = (ColorPickerModel)this.ColorPicker.GetModel();
            return colorSelectorModel;
        }
    }
}
