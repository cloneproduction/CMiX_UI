
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public class Sendable : ViewModel
    {
        public Sendable(string messageAddress, MessengerService messengerService)
        {
            MessageAddress = $"{messageAddress}{this.GetType().Name}/";
            MessengerService = messengerService;
        }

        public string MessageAddress { get; set; }
        public MessengerService MessengerService { get; set; }
    }
}