using CMiX.MVVM.ViewModels;
using CMiXPlayer.Jobs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FluentScheduler;

namespace CMiXPlayer.ViewModels
{
    public class Scheduler : ViewModel
    {
        public Scheduler(ObservableCollection<Device> devices)
        {
            Devices = devices;
            //SelectedDevice = new Device();
            Jobs = new ObservableCollection<IJob>();

            NewJobCommand = new RelayCommand(p => NewJob());
        }

        public ObservableCollection<Device> Devices { get; set; }

        public ObservableCollection<IJob> Jobs { get; set; }

        private Device _selecteddevice;
        public Device SelectedDevice
        {
            get { return _selecteddevice; }
            set { _selecteddevice = value; }
        }


        public ICommand NewJobCommand { get; set; }
        public ICommand StartJobCommand { get; set; }

        public List<Action<Schedule>> action { get; set; }

        private void NewJob()
        {
            //Job job = new Job(Devices);
            //Jobs.Add(job);
        }
    }
}