using CMiX.MVVM.ViewModels;
using CMiXPlayer.Jobs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FluentScheduler;

namespace CMiXPlayer.ViewModels
{
    public class Scheduler : Sendable
    {
        public Scheduler(ObservableCollection<Device> devices, ObservableCollection<IJob> runningJobs)
        {
            Devices = devices;
            RunningJobs = runningJobs;

            RemoveJobCommand = new RelayCommand(p => RemoveJob(p));
        }

        public ObservableCollection<Device> Devices { get; set; }

        public ObservableCollection<IJob> RunningJobs { get; set; }

        private Device _selecteddevice;
        public Device SelectedDevice
        {
            get { return _selecteddevice; }
            set { _selecteddevice = value; }
        }

        public ICommand RemoveJobCommand { get; set; }

        public List<Action<Schedule>> action { get; set; }

        private void RemoveJob(object job)
        {
            JobSendComposition j = job as JobSendComposition;
            JobManager.RemoveJob(j.Name);
            RunningJobs.Remove(j);
        }
    }
}