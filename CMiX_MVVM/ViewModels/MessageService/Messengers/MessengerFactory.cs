using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.MessageService
{
    public class MessengerFactory
    {
        int ID = 0;

        public Messenger CreateMessenger()
        {
            var messenger = new Messenger(ID);
            ID++;
            return messenger;
        }
    }
}