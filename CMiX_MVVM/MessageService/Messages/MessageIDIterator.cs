using System;
using System.Collections.Generic;

namespace CMiX.MVVM.MessageService
{
    public class MessageIDIterator : IIDIterator
    {
        public MessageIDIterator(Message message)
        {
            this.Message = message;
        }

        private int _current = -1;
        private int _step = 1;

        public Message Message { get; set; } //must be public because of Ceras


        public Guid First()
        {
            _current = 0;
            return Message.GetID(_current);
        }


        public List<Guid> IDs
        {
            get { return Message.IDs; }
        }

        public void Next()
        {
            _current += _step;
        }

        public Guid CurrentID
        {
            get { return Message.GetID(_current); }
        }

        // Gets whether iteration is complete
        public bool IsDone
        {
            get { return _current >= Message.Count; }
        }
    }
}
