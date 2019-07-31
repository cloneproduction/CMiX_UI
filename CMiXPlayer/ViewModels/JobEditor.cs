using CMiX.MVVM.ViewModels;
using CMiXPlayer.Jobs;
using FluentScheduler;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiXPlayer.ViewModels
{
    public class JobEditor : ViewModel
    {
        #region CONTRUCTORS
        public JobEditor(ObservableCollection<Device> devices, ObservableCollection<Playlist> playlists, ObservableCollection<IJob> runningJob)
        {
            Devices = devices;
            Playlists = playlists;
            ToRunType = new ToRunType();
            RunningJobs = runningJob;
            CreateJobCommand = new RelayCommand(p => CreateJob());
        }
        #endregion

        #region PROPERTIES
        public ICommand CreateJobCommand { get; }

        public ObservableCollection<Device> Devices { get; set; }
        ObservableCollection<IJob> RunningJobs { get; set; }


        private Device _selecteddevice;
        public Device SelectedDevice
        {
            get => _selecteddevice;
            set => SetAndNotify(ref _selecteddevice, value);
        }

        public ObservableCollection<Playlist> Playlists { get; set; }

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
        #endregion

        #region PRIVATE METHODS
        private void CreateJob()
        {
            if (SelectedPlaylist != null && SelectedDevice.OSCMessenger != null)
            {
                var j = new JobSendComposition(JobName, SelectedPlaylist, SelectedDevice.OSCMessenger);
                RunningJobs.Add(j);
                JobManager.AddJob(j, (s) => ToRunType.SetRunType(s.WithName(JobName)));
            }
        }
        #endregion
    }
}
