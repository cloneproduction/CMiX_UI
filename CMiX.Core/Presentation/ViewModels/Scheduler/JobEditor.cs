using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class JobEditor : ViewModel, IControl
    {
        public JobEditor(ObservableCollection<Playlist> playlists, JobScheduler jobScheduler)
        {

            JobScheduler = jobScheduler;
            Playlists = playlists;
            ToRunType = new ToRunType();

            CreateJobCommand = new RelayCommand(p => CreateJob());
        }


        public JobScheduler JobScheduler { get; set; }

        public ObservableCollection<Playlist> Playlists { get; set; }

        public ICommand CreateJobCommand { get; set; }

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
                var j = new JobNextComposition(JobName, SelectedPlaylist, (s) => ToRunType.SetRunType(s.WithName(JobName)));
                JobScheduler.AddJob(j);
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
