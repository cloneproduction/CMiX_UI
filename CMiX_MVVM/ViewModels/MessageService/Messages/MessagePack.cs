using CMiX.MVVM.ViewModels.MessageService;
using System.Collections.Generic;


namespace CMiX.MVVM.ViewModels.Messages
{
    public class MessagePack : IMessagePack
    {
        public MessagePack()
        {
            Messages = new List<IMessage>();
        }

        public List<IMessage> Messages { get; set; }

        public IMessageIterator CreateIterator()
        {
            var iterator = new MessageIterator(this);
            iterator.Reverse();
            return iterator;
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
