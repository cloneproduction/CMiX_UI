using CMiX.MVVM.Services;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels
{
    public interface IHandler
    {
        void HandleMessage(Message message, string parentMessageAddress);
        List<IHandler> Handlers { get; set; }
    }
}
