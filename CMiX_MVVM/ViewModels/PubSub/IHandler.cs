using System.Collections.Generic;
using CMiX.MVVM.Services.Message;

namespace CMiX.MVVM.ViewModels
{
    public interface IHandler
    {
        void HandleMessage(MessageReceived message, string parentMessageAddress);
        List<IHandler> Handlers { get; set; }
    }
}
