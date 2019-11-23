using Ceras;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using System;

namespace CMiX.Engine.ViewModel
{
    public class Object : ViewModel
    {
        public Object(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
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
                            ObjectModel objectModel = NetMQClient.ByteMessage.Payload as ObjectModel;
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

        public void PasteData(ObjectModel objectModel)
        {
            MessageAddress = objectModel.MessageAddress;
            this.Name = objectModel.Name;
            Geometry.PasteData(objectModel.GeometryModel);
            Texture.PasteData(objectModel.TextureModel);
            Coloration.PasteData(objectModel.ColorationModel);
        }
    }
}