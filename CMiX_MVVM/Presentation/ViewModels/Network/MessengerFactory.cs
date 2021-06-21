// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

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