using Ceras;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using System;
using System.Collections.Generic;

namespace CMiX.Engine.ViewModel
{
    public class Content : ViewModel
    {
        public Content(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
        : base(netMQClient, messageAddress, serializer)
        {
            Entities = new List<Entity>();
            Entity entity = new Entity(this.NetMQClient, this.MessageAddress + "Entity0", this.Serializer);
            entity.Name = "Entity 0";
            Entities.Add(entity);
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
                            ContentModel contentModel = NetMQClient.ByteMessage.Payload as ContentModel;
                            this.PasteData(contentModel);
                            //System.Console.WriteLine(MessageAddress + " " + Mode);
                        }
                        break;
                    case MessageCommand.OBJECT_ADD:
                        if(NetMQClient.ByteMessage.Payload != null)
                        {
                            EntityModel objectModel = NetMQClient.ByteMessage.Payload as EntityModel;
                            this.AddEntity(objectModel);
                        }
                        break;
                    case MessageCommand.OBJECT_DELETE:
                        if (NetMQClient.ByteMessage.Payload != null)
                        {
                            int index = (int)NetMQClient.ByteMessage.Payload;
                            Console.WriteLine("Delete Object with Index : " + index.ToString());
                            this.DeleteEntity(index);
                        }
                        break;
                }
            }
        }

        public List<Entity> Entities { get; set; }

        public void AddEntity(EntityModel entityModel)
        {
            Entity entity = new Entity(this.NetMQClient, this.MessageAddress + nameof(Entity), this.Serializer);
            entity.PasteData(entityModel);
            Entities.Add(entity);
            Console.WriteLine("AddObject messageaddress : " + entity.MessageAddress);
        }

        public void DeleteEntity(int index)
        {
            Console.WriteLine("DeleteLayer : " + Entities[index].MessageAddress);
            Entities.RemoveAt(index);
        }

        public void PasteData(ContentModel contentModel)
        {
            MessageAddress = contentModel.MessageAddress;
            Entities.Clear();
            foreach (EntityModel entity in contentModel.EntityModels)
            {
                Entity newEntity = new Entity(this.NetMQClient, this.MessageAddress, this.Serializer);
                newEntity.PasteData(entity);
            }
            Console.WriteLine("Content PasteData in Engine");
        }
    }
}