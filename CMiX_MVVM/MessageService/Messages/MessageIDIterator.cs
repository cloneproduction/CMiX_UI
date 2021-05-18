using System;

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

        public void Next()
        {
            _current += _step;
            //if (!IsDone)
            //    CurrentID = Message.GetID(_current);
            //else
            //    return Guid.Empty;
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
