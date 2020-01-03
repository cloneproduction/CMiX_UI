using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;
using System;
using System.Windows.Media;

namespace CMiX.Engine.ViewModel
{
    public class ColorPicker : IMessageReceiver
    {
        public ColorPicker(Receiver receiver, string messageAddress)
        {
            Receiver = receiver;
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
            //                ColorPickerModel colorPickerModel = NetMQClient.ByteMessage.Payload as ColorPickerModel;
            //                this.PasteData(colorPickerModel);
            //                //System.Console.WriteLine(MessageAddress + " " + Mode);
            //            }
            //            break;
            //    }
            //}
        }

        public Color SelectedColor { get; set; }
        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void PasteData(ColorPickerModel colorPickerModel)
        {
            MessageAddress = colorPickerModel.MessageAddress;
            SelectedColor = Utils.HexStringToColor(colorPickerModel.SelectedColor);
            Console.WriteLine(this.MessageAddress + " " + SelectedColor);
        }
    }
}
