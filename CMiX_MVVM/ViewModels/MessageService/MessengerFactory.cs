using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels.MessageService
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