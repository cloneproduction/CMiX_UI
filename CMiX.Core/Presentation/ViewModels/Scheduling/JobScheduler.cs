﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.ObjectModel;
using CMiX.Core.Models;
using CMiX.Core.Models.Scheduling;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Network.Messages;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduling
{
    public class JobScheduler : ViewModel, IControl
    {
        public JobScheduler(JobSchedulerModel jobSchedulerModel)
        {
            this.ID = jobSchedulerModel.ID;
            Schedules = new ObservableCollection<Job>();
        }


        public ObservableCollection<Job> Schedules { get; set; }
        public Guid ID { get; set; }


        public void AddJob(Job job)
        {
            JobManager.AddJob(job, job.Action);
            Schedules.Add(job);

            Communicator?.SendMessage(new MessageSchedulerJob(job.GetModel() as JobModel));
        }

        public void RemoveJob(Job job)
        {
            JobManager.RemoveJob(job.Name);
            Schedules.Remove(job);
        }


        private Communicator Communicator { get; set; }

        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new Communicator(this);
            Communicator.SetCommunicator(communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);
        }


        public IModel GetModel()
        {
            JobSchedulerModel jobSchedulerModel = new JobSchedulerModel();
            jobSchedulerModel.ID = this.ID;
            return jobSchedulerModel;
        }

        public void SetViewModel(IModel model)
        {
            JobSchedulerModel jobSchedulerModel = model as JobSchedulerModel;
            this.ID = jobSchedulerModel.ID;
        }
    }
}
