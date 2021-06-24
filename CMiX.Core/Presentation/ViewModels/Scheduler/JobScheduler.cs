// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.ObjectModel;
using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class JobScheduler : ViewModel, IControl
    {
        public JobScheduler()
        {
            Schedules = new ObservableCollection<Job>();
        }


        public ObservableCollection<Job> Schedules { get; set; }
        public Guid ID { get; set; }


        public void AddJob(Job job)
        {
            JobManager.AddJob(job, job.Action);
            Schedules.Add(job);

            //Communicator.SendMessageSchedulerJob(job.GetModel() as JobModel);
        }

        public void RemoveJob(Job job)
        {
            JobManager.RemoveJob(job.Name);
            Schedules.Remove(job);
        }


        private SchedulerCommunicator Communicator { get; set; }
        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new SchedulerCommunicator(this);
            Communicator.SetCommunicator(communicator);
        }
        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);
        }


        public IModel GetModel()
        {
            throw new NotImplementedException();
        }

        public void SetViewModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
