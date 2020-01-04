using System;
using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModel
{
    public class ColorSelector : ICopyPasteModel, IMessageReceiver
    {
        public ColorSelector(Receiver receiver, string messageAddress)
        {
            ColorPicker = new ColorPicker(receiver, this.MessageAddress + nameof(ColorPicker));
        }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            //string receivedAddress = NetMQClient.ByteMessage.MessageAddress;

            //if (receivedAddress == this.MessageAddress)
            //{
            //    MessageCommand command = NetMQClient.ByteMessage.Command;
            //    switch (command)
            //    {
            //        case MessageCommand.VIEWMODEL_UPDATE:
            //            if (NetMQClient.ByteMessage.Payload != null)
            //            {
            //                ColorSelectorModel colorSelectorModel = NetMQClient.ByteMessage.Payload as ColorSelectorModel;
            //                this.PasteData(colorSelectorModel);
            //                //System.Console.WriteLine(MessageAddress + " " + Mode);
            //            }
            //            break;
            //    }
            //}
        }
        public ColorPicker ColorPicker { get; set; }
        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void PasteModel(IModel model)
        {
            ColorSelectorModel colorSelectorModel = model as ColorSelectorModel;
            MessageAddress = colorSelectorModel.MessageAddress;
            ColorPicker.PasteModel(colorSelectorModel.ColorPickerModel);
        }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}