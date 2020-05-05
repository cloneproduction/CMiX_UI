using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels.MessageService
{
    public static class MessengerFactory
    {
        static int ID = 0;

        public static Messenger CreateMessenger()
        {
            var messenger = new Messenger(ID);
            ID++;
            return messenger;
        }
    }
}