using System;
using System.Collections.Generic;


namespace CMiX.MVVM.MessageService
{
    public abstract class Message
    {
        public Message()
        {
            IDs = new List<Guid>();
        }

        public List<Guid> IDs { get; set; }

        public IIDIterator CreateIterator()
        {
            var iterator = new MessageIDIterator(this);
            return iterator;
        }

        public int Count
        {
            get { return IDs.Count; }
        }

        public Guid GetID(int index)
        {
            return IDs[index];
        }

        public void AddID(Guid id)
        {
            IDs.Insert(0, id);
        }
    }
}
