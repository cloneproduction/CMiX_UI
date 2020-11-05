using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services.Message;

namespace CMiX.MVVM.ViewModels
{
    public interface IHandler
    {
        void HandleMessage(MessageReceived message, string parentMessageAddress);

        List<IHandler> GetHandlers();
    }
}
