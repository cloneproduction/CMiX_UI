using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public interface IMessage
    {
        object Obj { get; set; }
        string Address { get; set; }
        void Process(ViewModel viewModel);
    }
}
