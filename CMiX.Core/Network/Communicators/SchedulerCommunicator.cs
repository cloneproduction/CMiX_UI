// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Network.Messages;
using CMiX.Core.Presentation.ViewModels;
using FluentScheduler;

namespace CMiX.Core.Network.Communicators
{
    public sealed class SchedulerCommunicator : Communicator
    {
        public SchedulerCommunicator(IControl iDObject) : base()
        {
            IIDObject = iDObject;
        }

        public void SendMessageSchedulerJob(IJob job)
        {
            var message = new MessageSchedulerJob(job);
            this.SendMessage(message);
        }
    }
}
