using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.Core.Presentation.ViewModels;
using CMiXPlayer.Jobs;
using FluentScheduler;

namespace CMiXPlayer.ViewModels
{
    public class JobEditor : ViewModel
    {
        public JobEditor(ObservableCollection<Device> devices, ObservableCollection<Playlist> playlists, ObservableCollection<IJob> runningJob)
        {
            Devices = devices;
            Playlists = playlists;
            RunningJobs = runningJob;
            ToRunType = new ToRunType();

            CreateJobCommand = new RelayCommand(p => CreateJob());
        }

        public ICommand CreateJobCommand { get; }

        public ObservableCollection<Device> Devices { get; set; }
        public ObservableCollection<IJob> RunningJobs { get; set; }
        public ObservableCollection<Playlist> Playlists { get; set; }

        private Device _selecteddevice;
        public Device SelectedDevice
        {
            get => _selecteddevice;
            set => SetAndNotify(ref _selecteddevice, value);
        }

        private Playlist _selectedplaylist;
        public Playlist SelectedPlaylist
        {
            get => _selectedplaylist;
            set => SetAndNotify(ref _selectedplaylist, value);
        }

        private ToRunType _toruntype;
        public ToRunType ToRunType
        {
            get => _toruntype;
            set => SetAndNotify(ref _toruntype, value);
        }

        private string _jobName;
        public string JobName
        {
            get => _jobName;
            set => SetAndNotify(ref _jobName, value);
        }

        private void CreateJob()
        {
            //PROBLEM WITH OSCMESSENGER !
            //if (SelectedPlaylist != null && SelectedDevice.OSCSender != null)
            //{
            //    var j = new JobSendComposition(JobName, SelectedPlaylist, SelectedDevice);
            //    RunningJobs.Add(j);
            //    JobManager.AddJob(j, (s) => ToRunType.SetRunType(s.WithName(JobName)));
            //}
        }
    }
}