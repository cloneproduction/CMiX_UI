using System;

namespace CMiX.MVVM.MessageService
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
