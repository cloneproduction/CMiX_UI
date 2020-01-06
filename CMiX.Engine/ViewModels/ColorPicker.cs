using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

using System;
using System.Windows.Media;

namespace CMiX.Engine.ViewModels
{
    public class ColorPicker : ICopyPasteModel, IMessageReceiver
    {
        public ColorPicker(Receiver receiver, string messageAddress)
        {
            MessageAddress = $"{messageAddress}{nameof(ColorPicker)}";
            Receiver = receiver;
            Receiver.MessageReceived += OnMessageReceived;
        }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            Receiver.UpdateViewModel(MessageAddress, this);
        }

        public Color SelectedColor { get; set; }
        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void PasteModel(IModel model)
        {
            ColorPickerModel colorPickerModel = model as ColorPickerModel;
            MessageAddress = colorPickerModel.MessageAddress;
            SelectedColor = MVVM.Resources.Utils.HexStringToColor(colorPickerModel.SelectedColor);
            Console.WriteLine(this.MessageAddress + " " + SelectedColor);
        }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}