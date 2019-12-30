using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public interface ISendable
    {
        string MessageAddress { get; set; }
        MessageService MessageService { get; set; }
    }
}