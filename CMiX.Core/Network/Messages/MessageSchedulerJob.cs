// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using FluentScheduler;

namespace CMiX.Core.Network.Messages
{
    public sealed class MessageSchedulerJob : Message
    {
        public MessageSchedulerJob()
        {

        }

        public MessageSchedulerJob(IJob job)
        {
            Job = job;
        }

        public IJob Job { get; set; }

        public override void Process<T>(T receiver)
        {
            throw new NotImplementedException();
        }
    }
}
