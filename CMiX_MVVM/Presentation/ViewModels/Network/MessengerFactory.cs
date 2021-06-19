using CMiX.Studio.ViewModels;

namespace CMiX.Core.Presentation.ViewModels
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