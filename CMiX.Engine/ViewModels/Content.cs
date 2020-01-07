using CMiX.MVVM;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;
using System.Collections.Generic;

namespace CMiX.Engine.ViewModels
{
    public class Content : IMessageReceiver, ICopyPasteModel<ContentModel>
    {
        public Content(Receiver receiver, string messageAddress)
        {
            MessageAddress = $"{messageAddress}{nameof(Content)}/";
            Receiver = receiver;
            Receiver.MessageReceived += OnMessageReceived;

            Entities = new List<Entity>();
            Entity entity = new Entity(receiver, MessageAddress + "Entity0");
            entity.Name = "Entity 0";
            Entities.Add(entity);
        }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            Receiver.UpdateViewModel(MessageAddress, this);
            if (MessageAddress == Receiver.ReceivedAddress && Receiver.ReceivedData != null)
            {
                MessageCommand command = Receiver.ReceivedCommand;
                switch (command)
                {
                    case MessageCommand.ENTITY_ADD:
                        EntityModel objectModel = Receiver.ReceivedData as EntityModel;
                        this.AddEntity(objectModel);
                        Console.WriteLine("Added Object with ID : " + objectModel.Name);
                        break;

                    case MessageCommand.ENTITY_DELETE:
                        int index = (int)Receiver.ReceivedData;
                        Console.WriteLine("Delete Object with Index : " + index.ToString());
                        this.DeleteEntity(index);
                        break;
                }
            }
        }

        public List<Entity> Entities { get; set; }
        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void AddEntity(EntityModel entityModel)
        {
            Entity entity = new Entity(Receiver, this.MessageAddress + nameof(Entity));
            entity.PasteModel(entityModel);
            Entities.Add(entity);
        }

        public void DeleteEntity(int index)
        {
            Entities.RemoveAt(index);
        }

        public void PasteModel(ContentModel contentModel)
        {
            Entities.Clear();
            foreach (EntityModel entity in contentModel.EntityModels)
            {
                Entity newEntity = new Entity(Receiver, this.MessageAddress);
                newEntity.PasteModel(entity);
            }
        }

        public void CopyModel(ContentModel contentModel)
        {
            throw new NotImplementedException();
        }
    }
}