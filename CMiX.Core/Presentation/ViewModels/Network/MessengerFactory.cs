// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace CMiX.Core.Presentation.ViewModels.Network
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
