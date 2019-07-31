using System;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using CMiXPlayer.ViewModels;
using FluentScheduler;

namespace CMiXPlayer.Jobs
{
    public class JobSendComposition : ViewModel, IJob
    {
        public JobSendComposition()
        {

        }

        public JobSendComposition(string name, Playlist playlist, OSCMessenger oscmessenger)
        {
            Name = name;
            Playlist = playlist;
            OSCMessenger = oscmessenger;
        }
        public OSCMessenger OSCMessenger { get; set; }

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

        public CompositionModel _currentComposition;
        public CompositionModel CurrentComposition
        {
            get => _currentComposition;
            set => SetAndNotify(ref _currentComposition, value);
        }


        int CompositionIndex = 0;

        public void Execute()
        {
            if (CompositionIndex <= Playlist.Compositions.Count - 1)
            {
                CompositionModel compositionmodel = Playlist.Compositions[CompositionIndex];
                CurrentComposition = compositionmodel;
                OSCMessenger.SendMessage("/CompositionReloaded", true);
                OSCMessenger.QueueObject(compositionmodel);
                OSCMessenger.SendQueue();
                CompositionIndex += 1;
            }
            else
            {
                CompositionIndex = 0;
            }
        }
    }
}