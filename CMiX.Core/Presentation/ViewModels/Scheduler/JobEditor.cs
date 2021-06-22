using System.Collections.ObjectModel;
using System.Windows.Input;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class JobEditor : ViewModel
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

        private void CreateJob()
        {

            if (SelectedPlaylist != null)
            {
                var j = new JobSendComposition(JobName, SelectedPlaylist);
                RunningJobs.Add(j);
                JobManager.AddJob(j, (s) => ToRunType.SetRunType(s.WithName(JobName)));
            }
        }
    }
}
