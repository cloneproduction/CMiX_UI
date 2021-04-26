using CMiX.MVVM.ViewModels.MessageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.Messages
{
    public interface IMessageAggregator
    {
        IMessageIterator CreateIterator();
        void AddMessage(IMessage message);
    }
}
