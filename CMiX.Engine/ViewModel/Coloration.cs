using System;
using Ceras;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModel
{
    public class Coloration : IMessageReceiver
    {
        public Coloration(Receiver receiver, string messageAddress)
        {
            Receiver = receiver;
            ColorSelector = new ColorSelector(receiver, this.MessageAddress + nameof(ColorSelector));
        }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            //string receivedAddress = NetMQClient.ByteMessage.MessageAddress;

            //if (receivedAddress == this.MessageAddress)
            //{
            //    MessageCommand command = NetMQClient.ByteMessage.Command;
            //    System.Console.WriteLine("receivedAddress : " + receivedAddress);
            //    System.Console.WriteLine("this.MessageAddress : " + this.MessageAddress);
            //    switch (command)
            //    {
            //        case MessageCommand.VIEWMODEL_UPDATE:
            //            if (NetMQClient.ByteMessage.Payload != null)
            //            {
            //                ColorationModel colorationModel = NetMQClient.ByteMessage.Payload as ColorationModel;
            //                this.PasteData(colorationModel);
            //            }
            //            break;
            //    }
            //}
        }

        public ColorSelector ColorSelector { get; set; }
        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void PasteData(ColorationModel colorationModel)
        {
            MessageAddress = colorationModel.MessageAddress;
            ColorSelector.PasteData(colorationModel.ColorSelectorModel);
        }
    }
}
