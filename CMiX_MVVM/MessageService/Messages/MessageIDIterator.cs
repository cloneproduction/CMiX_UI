using System;
using System.Collections.Generic;

namespace CMiX.Core.MessageService
{
    public class MessageIDIterator : IIDIterator
    {
        public MessageIDIterator(Message message)
        {
            this._message = message;
        }

        private int _current = -1;
        private int _step = 1;

        private Message _message;// get; set; } //must be public because of Ceras
        public Message Message
        {
            get { return _message; }
        }

        public Guid First()
        {
            _current = 0;
            return _message.GetID(_current);
        }


        public List<Guid> IDs
        {
            get { return _message.IDs; }
        }

        public void Next()
        {
            _current += _step;
        }

        public void Process<T>(T receiver)
        {
            Message.Process(receiver);
        }

        public Guid CurrentID
        {
            get { return _message.GetID(_current); }
        }

        // Gets whether iteration is complete
        public bool IsDone
        {
            get { return _current >= _message.Count; }
        }
    }
}
