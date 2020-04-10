using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.ViewModels
{
    public class Sendable : ViewModel
    {
        public Sendable(string messageAddress, MessageService messageService)
        {
            MessageAddress = $"{messageAddress}{this.GetType().Name}/";
            MessageService = messageService;
        }

        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
    }
}