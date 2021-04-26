using CMiX.MVVM.ViewModels.MessageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.Messages
{
    public class MessageIterator<T> : IMessageIterator
    {
        public MessageIterator(MessageAggregator messageAggregator)
        {
            this.MessageAggregator = messageAggregator;
        }

        private int _current = 0;
        private int _step = 1;

        private MessageAggregator MessageAggregator { get; set; }

        //public IMessage First()
        //{
        //    _current = 0;
        //    return MessageAggregator.GetMessage(_current);
        //}

        //public IMessage Next()
        //{
        //    _current += _step;
        //    if (!IsDone)
        //        return MessageAggregator.GetMessage(_current);
        //    else
        //        return null;
        //}

        //public IMessage CurrentMessage
        //{
        //    get { return MessageAggregator.GetMessage(_current); }
        //}

        //// Gets whether iteration is complete
        //public bool IsDone
        //{
        //    get { return _current >= MessageAggregator.Count; }
        //}
    }
}
