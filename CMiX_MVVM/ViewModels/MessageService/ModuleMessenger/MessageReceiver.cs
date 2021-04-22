using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public abstract class MessageReceiver<T>
    {
        public MessageReceiver()
        {
            MessageCommunicators = new Dictionary<Guid, T>();
        }

        private Dictionary<Guid, T> MessageCommunicators { get; set; }

        public abstract T GetMessageProcessor(Guid id);
        //{
        //    if (MessageCommunicators.ContainsKey(id))
        //        return MessageCommunicators[id];
        //    return null;
        //}

        public void RegisterReceiver(T messageCommunicator, Guid messageCommunicatorID)
        {
            if (!MessageCommunicators.ContainsKey(messageCommunicatorID))
                MessageCommunicators.Add(messageCommunicatorID, messageCommunicator);
        }

        public void UnregisterReceiver(Guid messageCommunicatorID)
        {
            MessageCommunicators.Remove(messageCommunicatorID);
        }

        public abstract void ReceiveMessage(IMessage message);
    }
}
