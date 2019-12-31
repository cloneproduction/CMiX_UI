using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public interface ISendable
    {
        string MessageAddress { get; set; }
        Messenger Messenger { get; set; }
    }
}