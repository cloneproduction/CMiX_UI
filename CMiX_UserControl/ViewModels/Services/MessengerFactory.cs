using System.Collections.Generic;

namespace CMiX.Studio.ViewModels.MessageService
{
    public static class MessengerFactory
    {
        static int ID = 0;

        public static Messenger CreateMessenger(List<string> addresses)
        {
            var messenger = new Messenger(ID, addresses);
            ID++;
            return messenger;
        }
    }
}