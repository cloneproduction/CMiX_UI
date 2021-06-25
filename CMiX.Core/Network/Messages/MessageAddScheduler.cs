﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models.Scheduling;
using CMiX.Core.Presentation.ViewModels.Scheduling;

namespace CMiX.Core.Network.Messages
{
    public class MessageAddScheduler : Message
    {
        public MessageAddScheduler()
        {

        }

        public MessageAddScheduler(CompositionSchedulerModel schedulerModel)
        {
            SchedulerModel = schedulerModel;
        }


        public CompositionSchedulerModel SchedulerModel { get; set; }

        public override void Process<T>(T receiver)
        {
            SchedulerManager schedulerManager = receiver as SchedulerManager;
            schedulerManager.CreateScheduler(SchedulerModel);
        }
    }
}
