using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.Engine.ViewModel
{
    public class Texture : IMessageReceiver
    {
        public Texture(Receiver receiver, string messageAddress)
        {

        }

        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

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
            //                TextureModel textureModel = NetMQClient.ByteMessage.Payload as TextureModel;
            //                this.PasteData(textureModel);
            //                //System.Console.WriteLine(MessageAddress + " " + Mode);
            //            }
            //            break;
            //    }
            //}
        }

        public void PasteData(TextureModel textureModel)
        {
            MessageAddress = textureModel.MessageAddress;
        }
    }
}
