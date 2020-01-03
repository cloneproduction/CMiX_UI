using Ceras;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.Engine.ViewModel
{
    public class Entity : IMessageReceiver
    {
        public Entity(Receiver receiver, string messageAddress)
        {
            Receiver = receiver;
            Geometry = new Geometry(Receiver, this.MessageAddress + nameof(Geometry));
            Texture = new Texture(Receiver, this.MessageAddress + nameof(Texture));
            Coloration = new Coloration(Receiver, this.MessageAddress + nameof(Coloration));
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
            //                EntityModel objectModel = NetMQClient.ByteMessage.Payload as EntityModel;
            //                this.PasteData(objectModel);
            //                //System.Console.WriteLine(MessageAddress + " " + Mode);
            //            }
            //            break;
            //    }
            //}
        }
        public string Name { get; set; }
        public Geometry Geometry { get; set; }
        public Texture Texture { get; set; }
        public Coloration Coloration { get; set; }
        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void PasteData(EntityModel entityModel)
        {
            MessageAddress = entityModel.MessageAddress;
            this.Name = entityModel.Name;
            Geometry.PasteData(entityModel.GeometryModel);
            Texture.PasteData(entityModel.TextureModel);
            Coloration.PasteData(entityModel.ColorationModel);
        }
    }
}