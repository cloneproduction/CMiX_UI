using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.MVVM.Message;

namespace CMiX.MVVM.ViewModels
{
    public interface IHandler
    {
        void HandleMessage(Message.Message message, string parentMessageAddress);

        List<IHandler> GetHandlers();
    }
}
