// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.ObjectModel;
using CMiX.Core.Network.Messages;
using CMiX.Core.Presentation.ViewModels.Components;
using CMiX.Core.Presentation.ViewModels.Network;

namespace CMiX.Core.Presentation.ViewModels
{
    public class MessageService : ViewModel, IMessageSender
    {
        public MessageService(Project project)
        {
            _project = project;
            project.Communicator.MessageSent += Communicator_MessageSent;
        }

        private readonly Project _project;
        public ObservableCollection<Messenger> Messengers { get; set; }

        private void Communicator_MessageSent(object sender, MessageEventArgs e)
        {
            this.SendMessage(e.Message);
        }

        public void SendMessage(Message message)
        {
            foreach (var messenger in _project.Messengers)
            {
                messenger.SendMessage(message);
                Console.WriteLine("DataSender SendMessage");
            }
        }
    }
}
