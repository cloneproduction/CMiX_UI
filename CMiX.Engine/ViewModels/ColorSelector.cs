using System;
using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModels
{
    public class ColorSelector : ICopyPasteModel, IMessageReceiver
    {
        public ColorSelector(Receiver receiver, string messageAddress)
        {
            MessageAddress = $"{messageAddress}{nameof(ColorSelector)}/";
            Receiver = receiver;
            Receiver.MessageReceived += OnMessageReceived;
            ColorPicker = new ColorPicker(receiver, MessageAddress);
        }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            Receiver.UpdateViewModel(MessageAddress, this);
        }

        public ColorPicker ColorPicker { get; set; }
        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void PasteModel(ColorSelectorModel colorSelectorModel)
        {
            ColorPicker.PasteModel(colorSelectorModel.ColorPickerModel);
        }

        public void CopyModel(ColorSelectorModel colorSelectorModel)
        {
            throw new NotImplementedException();
        }
    }
}