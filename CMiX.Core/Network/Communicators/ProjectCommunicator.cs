﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Network.Messages;
using CMiX.Core.Presentation.ViewModels.Components;

namespace CMiX.Core.Network.Communicators
{
    public class ProjectCommunicator : Communicator
    {
        public ProjectCommunicator(Project project) : base(project)
        {
            Project = project;
        }

        private Project Project { get; set; }

        public override void SendMessage(Message message)
        {
            foreach (var messenger in Project.Messengers)
            {
                messenger.SendMessage(message);
            }
        }
    }
}
