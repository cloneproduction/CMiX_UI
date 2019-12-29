using Ceras;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using System;

namespace CMiX.Engine.ViewModel
{
    public class Entity : ViewModel
    {
        public Entity(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
        : base(netMQClient, messageAddress, serializer)
        {
            Geometry = new Geometry(this.NetMQClient, this.MessageAddress + nameof(Geometry), this.Serializer);
            Texture = new Texture(this.NetMQClient, this.MessageAddress + nameof(Texture), this.Serializer);
            Coloration = new Coloration(this.NetMQClient, this.MessageAddress + nameof(Coloration), this.Serializer);
        }

        public override void ByteReceived()
        {
            string receivedAddress = NetMQClient.ByteMessage.MessageAddress;

            if (receivedAddress == this.MessageAddress)
            {
                MessageCommand command = NetMQClient.ByteMessage.Command;
                switch (command)
                {
                    case MessageCommand.VIEWMODEL_UPDATE:
                        if (NetMQClient.ByteMessage.Payload != null)
                        {
                            EntityModel objectModel = NetMQClient.ByteMessage.Payload as EntityModel;
                            this.PasteData(objectModel);
                            //System.Console.WriteLine(MessageAddress + " " + Mode);
                        }
                        break;
                }
            }
        }
        public string Name { get; set; }
        public Geometry Geometry { get; set; }
        public Texture Texture { get; set; }
        public Coloration Coloration { get; set; }

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