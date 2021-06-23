// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.ObjectModel;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class JobScheduler : ViewModel
    {
        public JobScheduler()
        {
            Schedules = new ObservableCollection<Job>();
        }

        public ObservableCollection<Job> Schedules { get; set; }

        public void AddJob(Job job)
        {
            JobManager.AddJob(job, job.Action);
            Schedules.Add(job);
        }

        public void RemoveJob(Job job)
        {
            JobManager.RemoveJob(job.Name);
            Schedules.Remove(job);
        }
    }
}
