using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public static class MessengerFactory
    {

        static int MessengerID = 0;

        public static Messenger CreateMessenger()
        {
            var messenger = new Messenger();
            MessengerID++;
            return messenger;
        }
    }
}
