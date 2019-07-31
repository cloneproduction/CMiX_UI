using System;
using System.Windows.Input;
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
            NextComposition = playlist.Compositions[0];

            SendNextCommand = new RelayCommand(p => SendNext());
        }

        public OSCMessenger OSCMessenger { get; set; }

        public ICommand SendNextCommand { get; }
        public ICommand PreviousJobCommand { get; }
        public ICommand PauseJobCommand { get; }

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

        public CompositionModel _nextComposition;
        public CompositionModel NextComposition
        {
            get => _nextComposition;
            set => SetAndNotify(ref _nextComposition, value);
        }

        int NextCompositionIndex = 0;

        public void Execute()
        {
            SendNext();
        }

        public void Send()
        {
            CurrentComposition = NextComposition;
            OSCMessenger.SendMessage("/CompositionReloaded", true);
            OSCMessenger.QueueObject(CurrentComposition);
            OSCMessenger.SendQueue();
        }

        public void Next()
        {
            if (NextCompositionIndex < Playlist.Compositions.Count - 1)
            {
                NextCompositionIndex += 1;
            }
            else
            {
                NextCompositionIndex = 0;
            }
            NextComposition = Playlist.Compositions[NextCompositionIndex];
        }

        public void SendNext()
        {
            Send();
            Next();
        }
    }
}