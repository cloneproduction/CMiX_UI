using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Beat
{
    public interface IBeat : IMessageProcessor
    {
        MasterBeat MasterBeat { get; set; }
    }
}
