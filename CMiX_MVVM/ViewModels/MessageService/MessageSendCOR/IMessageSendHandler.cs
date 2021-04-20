using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.MessageService.MessageSendCOR
{
    public interface IMessageSendHandler
    {
        IMessageSendHandler SetSender(IMessageSendHandler handler);
        void SendMessage(IMessage message);
    }
}
