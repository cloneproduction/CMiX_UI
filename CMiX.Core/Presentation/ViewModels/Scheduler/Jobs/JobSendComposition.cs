using CMiX.Core.Presentation.ViewModels.Components;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class JobSendComposition : ViewModel, IJob
    {
        public JobSendComposition(string name, Playlist playlist)
        {
            Name = name;
            Playlist = playlist;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private Playlist _playlist;
        public Playlist Playlist
        {
            get => _playlist;
            set => SetAndNotify(ref _playlist, value);
        }

        public Composition _currentComposition;
        public Composition CurrentComposition
        {
            get => _currentComposition;
            set => SetAndNotify(ref _currentComposition, value);
        }

        public bool Pause { get; set; }

        int CompositionIndex = -1;


        public void Execute()
        {
            if (!Pause)
                Next();
        }


        public void Next()
        {
            if (CompositionIndex >= Playlist.Compositions.Count - 1)
                CompositionIndex = 0;
            else
                CompositionIndex += 1;
            CurrentComposition = Playlist.Compositions[CompositionIndex];
        }

        public void Previous()
        {
            if (CompositionIndex <= 0)
                CompositionIndex = Playlist.Compositions.Count - 1;
            else
                CompositionIndex -= 1;
            CurrentComposition = Playlist.Compositions[CompositionIndex];
        }
    }
}
