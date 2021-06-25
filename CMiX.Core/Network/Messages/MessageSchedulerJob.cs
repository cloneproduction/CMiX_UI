// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models.Scheduling;

namespace CMiX.Core.Network.Messages
{
    public sealed class MessageSchedulerJob : Message
    {
        public MessageSchedulerJob()
        {

        }

        public MessageSchedulerJob(JobModel jobModel)
        {
            JobModel = jobModel;
        }

        public JobModel JobModel { get; set; }

        public override void Process<T>(T receiver)
        {
            //Project project = receiver as Project;
            //JobNextComposition jobNextComposition = new JobNextComposition();
            //project.CompositionScheduler.JobScheduler.AddJob(jobNextComposition);
        }
    }
}
