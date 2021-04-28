﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components;
using CMiX.MVVM.ViewModels.Components.Messages;
using CMiX.MVVM.ViewModels.Messages;
using System;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class ComponentSender : IMessageSender
    {
        public ComponentSender()
        {

        }

        private IMessageSender MessageSender;
        public IMessageSender SetSender(IMessageSender messageSender)
        {
            MessageSender = messageSender;
            return messageSender;
        }

        public void SendMessageAddComponent(Guid parentID, Component newComponent)
        {
            IMessage message = new MessageAddComponent(parentID, newComponent.GetModel() as IComponentModel);

            MessageAggregator messageAggregator = new MessageAggregator();
            messageAggregator.AddMessage(message);
            this.SendMessageAggregator(messageAggregator);
            Console.WriteLine("ComponentMessageSender SendMessageAdd");
        }

        public void SendMessageRemoveComponent(Guid parentID, int index)
        {
            IMessage message = new MessageRemoveComponent(parentID, index);

            MessageAggregator messageAggregator = new MessageAggregator();
            messageAggregator.AddMessage(message);
            this.SendMessageAggregator(messageAggregator);
            Console.WriteLine("ComponentMessageSender SendMessageRemove");
        }


        public void SendMessageAggregator(IMessageAggregator messageAggregator)
        {
            Console.WriteLine("ComponentMessageSender SendMessageAggregator");
            MessageSender?.SendMessageAggregator(messageAggregator);

        }
    }
}