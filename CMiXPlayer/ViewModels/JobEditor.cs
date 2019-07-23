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
        public JobEditor(ObservableCollection<Device> devices, ObservableCollection<Playlist> playlists)
        {
            Devices = devices;
            Playlists = playlists;
            ToRunType = new ToRunType();

            CreateJobCommand = new RelayCommand(p => CreateJob());
        }
        #endregion

        #region PROPERTIES
        public ICommand CreateJobCommand { get; }

        public ObservableCollection<Device> Devices { get; set; }

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

        private string _schedulename;
        public string ScheduleName
        {
            get => _schedulename;
            set => SetAndNotify(ref _schedulename, value);
        }
        #endregion

        #region PRIVATE METHODS
        private void CreateJob()
        {
            Console.WriteLine("Create Job");
            if (SelectedPlaylist != null && SelectedDevice.OSCMessenger != null)
            {
                var j = new JobSendComposition(SelectedPlaylist, SelectedDevice.OSCMessenger);
                JobManager.AddJob(j, (s) => ToRunType.SetRunType(s.WithName(ScheduleName)));
            }
        }
        #endregion
    }
}
