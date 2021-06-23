using System;
using System.Collections.Generic;
using System.Windows.Input;
using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class JobsOverview : ViewModel, IControl
    {
        public JobsOverview(JobScheduler jobScheduler)
        {
            JobScheduler = jobScheduler;
            RemoveJobCommand = new RelayCommand(p => RemoveJob(p as Job));
        }


        public ICommand RemoveJobCommand { get; set; }

        public JobScheduler JobScheduler { get; set; }
        public ControlCommunicator Communicator { get; set; }
        public List<Action<Schedule>> action { get; set; }

        public Guid ID { get; set; }

        private void RemoveJob(Job job)
        {
            JobScheduler.RemoveJob(job);
        }


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);
        }


        public void SetViewModel(IModel model)
        {
            throw new NotImplementedException();
        }

        public IModel GetModel()
        {
            throw new NotImplementedException();
        }
    }
}
