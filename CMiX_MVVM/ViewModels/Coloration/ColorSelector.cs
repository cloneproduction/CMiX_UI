﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
using System.Windows.Media;

namespace CMiX.MVVM.ViewModels
{
    public class ColorSelector : MessageCommunicator
    {
        public ColorSelector(IMessageDispatcher messageDispatcher, ColorSelectorModel colorSelectorModel) 
            : base (messageDispatcher, colorSelectorModel)
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
