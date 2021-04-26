using System;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public class MessageEmpty : IMessage
    {
        public MessageEmpty()
        {

        }
        public MessageEmpty(Guid id)
        {
            ComponentID = id;
        }
        public Guid ComponentID { get; set; }
    }
}
