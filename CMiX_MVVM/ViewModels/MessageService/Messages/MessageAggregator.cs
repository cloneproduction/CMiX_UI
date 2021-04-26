﻿using CMiX.MVVM.ViewModels.MessageService;
using System.Collections.Generic;


namespace CMiX.MVVM.ViewModels.Messages
{
    public class MessageAggregator : IMessageAggregator
    {
        public MessageAggregator()
        {
            Messages = new List<IMessage>();
        }

        List<IMessage> Messages { get; set; }

        public IMessageIterator CreateIterator()
        {
            return new MessageIterator(this);
        }

        public int Count
        {
            get { return Messages.Count; }
        }

        public IMessage GetMessage(int index)
        {
            return Messages[index];
        }
        public void AddMessage(IMessage message)
        {
            Messages.Add(message);
        }
    }
}
