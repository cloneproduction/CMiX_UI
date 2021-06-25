// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models.Scheduling;
using CMiX.Core.Network.Messages;
using CMiX.Core.Presentation.ViewModels;

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

        public void SendMessageAddScheduler(CompositionSchedulerModel compositionSchedulerModel)
        {
            var message = new MessageAddScheduler(compositionSchedulerModel);
            this.SendMessage(message);
        }

        public void SendMessageSelectedSchedulerIndex(int index)
        {
            var message = new MessageSelectedSchedulerIndex(index);
            this.SendMessage(message);
        }
    }
}
