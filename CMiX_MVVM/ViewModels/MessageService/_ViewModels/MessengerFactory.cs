using CMiX.Studio.ViewModels;

namespace CMiX.MVVM.ViewModels
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