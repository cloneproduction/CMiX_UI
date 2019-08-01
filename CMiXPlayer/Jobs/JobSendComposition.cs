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

        public JobSendComposition(string name, Playlist playlist, Device device)
        {
            Name = name;
            Playlist = playlist;
            Device = device;
            OSCMessenger = device.OSCMessenger;

            Init();

            SendNextCommand = new RelayCommand(p => SendNext());
            SendPreviousCommand = new RelayCommand(p => SendPrevious());
        }

        public OSCMessenger OSCMessenger { get; set; }

        public ICommand SendNextCommand { get; }
        public ICommand SendPreviousCommand { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private Device _device;
        public Device Device
        {
            get => _device;
            set => SetAndNotify(ref _device, value);
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

        public bool Pause { get; set; }

        int NextCompositionIndex = 0;

        public void Execute()
        {
            if(!Pause)
                SendNext();
        }

        public void Send()
        {
            if(NextComposition != null)
            {
                CurrentComposition = NextComposition;
                OSCMessenger.SendMessage("/CompositionReloaded", true);
                OSCMessenger.QueueObject(CurrentComposition);
                OSCMessenger.SendQueue();
            }
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
            if (Playlist.Compositions.Count > 0)
                NextComposition = Playlist.Compositions[NextCompositionIndex];
        }

        public void Previous()
        {
            if (NextCompositionIndex > 0)
            {
                NextCompositionIndex -= 1;
            }
            else
            {
                NextCompositionIndex = Playlist.Compositions.Count - 1;
            }
            if (Playlist.Compositions.Count > 0)
                NextComposition = Playlist.Compositions[NextCompositionIndex];
        }

        public void SendNext()
        {
            Send();
            Next();
        }

        public void SendPrevious()
        {
            Send();
            Previous();
        }

        public void Init()
        {
            if (Playlist.Compositions.Count > 0)
                NextComposition = Playlist.Compositions[0];
        }
    }
}