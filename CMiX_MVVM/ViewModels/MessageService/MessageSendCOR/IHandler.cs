using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.MessageService.MessageSendCOR
{
    public interface IHandler
    {
        IHandler SetNextSender(IHandler handler);
        void SendMessage(IMessage message);
    }
}
