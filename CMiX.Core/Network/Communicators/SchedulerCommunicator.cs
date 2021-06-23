// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models.Scheduler;
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

        public void SendMessageSchedulerJob(JobModel jobModel)
        {
            var message = new MessageSchedulerJob(jobModel);
            this.SendMessage(message);
        }
    }
}
