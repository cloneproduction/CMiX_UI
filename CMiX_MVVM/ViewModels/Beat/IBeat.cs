using CMiX.MVVM.ViewModels.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels.Beat
{
    public interface IBeat : IMessageProcessor
    {
        MasterBeat MasterBeat { get; set; }
    }
}
