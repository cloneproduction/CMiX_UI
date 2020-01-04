using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.Engine.ViewModel
{
    public class Entity : IMessageReceiver, ICopyPasteModel
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
        public string MessageAddress { get; set; }
        public Geometry Geometry { get; set; }
        public Texture Texture { get; set; }
        public Coloration Coloration { get; set; }
        public Receiver Receiver { get; set; }

        public void PasteModel(IModel model)
        {
            EntityModel entityModel = model as EntityModel;
            MessageAddress = entityModel.MessageAddress;
            this.Name = entityModel.Name;
            Geometry.PasteModel(entityModel.GeometryModel);
            Texture.PasteModel(entityModel.TextureModel);
            Coloration.PasteModel(entityModel.ColorationModel);
        }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}