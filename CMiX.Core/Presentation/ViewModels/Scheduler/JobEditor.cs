using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class JobEditor : ViewModel, IControl
    {
        public JobEditor(ObservableCollection<Playlist> playlists)
        {

            Playlists = playlists;
            RunningJobs = new ObservableCollection<IJob>();
            ToRunType = new ToRunType();

            CreateJobCommand = new RelayCommand(p => CreateJob());
        }

        public ICommand CreateJobCommand { get; }


        public ObservableCollection<IJob> RunningJobs { get; set; }
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
        public Guid ID { get; set; }

        private void CreateJob()
        {
            if (SelectedPlaylist != null)
            {
                var j = new JobNextComposition(JobName, SelectedPlaylist);
                RunningJobs.Add(j);
                JobManager.AddJob(j, (s) => ToRunType.SetRunType(s.WithName(JobName)));

            }
        }


        public ControlCommunicator Communicator { get; set; }
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
