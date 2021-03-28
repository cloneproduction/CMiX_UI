using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Tools;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System.Windows.Media;

namespace CMiX.MVVM.ViewModels
{
    public class ColorSelector : MessageCommunicator
    {
        //public ColorSelector(IMessageProcessor parentSender) : base (parentSender)
        //{
        //    ColorPicker = new ColorPicker(this);
        //    this.SelectedColor = Colors.Red;
        //}

        public ColorSelector(MessageDispatcher messageDispatcher, ColorSelectorModel colorSelectorModel) : base (messageDispatcher)
        {
            ColorPicker = new ColorPicker(messageDispatcher, colorSelectorModel.ColorPickerModel);
            //this.SelectedColor = Utils.HexStringToColor(colorSelectorModel.ColorPickerModel.SelectedColor);
        }


        public ColorPicker ColorPicker { get; set; }

        private Color _selectedColor;
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                SetAndNotify(ref _selectedColor, value);

                RaiseMessageNotification();
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
            colorSelectorModel.ColorPickerModel = (ColorPickerModel)ColorPicker.GetModel();
            return colorSelectorModel;
        }
    }
}
