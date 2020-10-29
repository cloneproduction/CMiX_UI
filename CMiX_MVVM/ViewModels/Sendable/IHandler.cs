using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public interface IHandler
    {
        void HandleMessage(Message.Message message, string parentMessageAddress);

        List<IHandler> GetHandlers();

        //void SetViewModel(Model model);
    }
}
