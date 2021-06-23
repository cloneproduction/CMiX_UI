using System;
using CMiX.Core.Presentation.ViewModels.Components;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class JobNextComposition : Job
    {
        public JobNextComposition(string name, Playlist playlist, Action<Schedule> action)
        {
            Name = name;
            Action = action;
            Playlist = playlist;
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


        public override void Execute()
        {
            if (!Pause)
                Next();
            var schedule = JobManager.GetSchedule(this.Name);
            this.NextRun = schedule.NextRun;
            this.Disabled = schedule.Disabled;
        }

        public void Next()
        {
            CompositionIndex += 1;

            if (CompositionIndex >= Playlist.Compositions.Count - 1)
                CompositionIndex = 0;

            CurrentComposition = Playlist.Compositions[CompositionIndex];
        }

        public void Previous()
        {
            CompositionIndex -= 1;
            if (CompositionIndex <= 0)
                CompositionIndex = Playlist.Compositions.Count - 1;

            CurrentComposition = Playlist.Compositions[CompositionIndex];
        }
    }
}
