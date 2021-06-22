using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class Scheduler : ViewModel, IControl
    {
        public Scheduler()
        {
            //RunningJobs = runningJobs;

            RemoveJobCommand = new RelayCommand(p => RemoveJob(p));
        }


        public ObservableCollection<IJob> RunningJobs { get; set; }


        public ICommand RemoveJobCommand { get; set; }

        public ControlCommunicator Communicator { get; set; }
        public List<Action<Schedule>> action { get; set; }

        public Guid ID { get; set; }

        private void RemoveJob(object job)
        {
            JobSendComposition j = job as JobSendComposition;
            JobManager.RemoveJob(j.Name);
            RunningJobs.Remove(j);
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
