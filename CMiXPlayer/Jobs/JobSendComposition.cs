using System;
using System.Windows.Input;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using CMiXPlayer.ViewModels;
using FluentScheduler;

namespace CMiXPlayer.Jobs
{
    public class JobSendComposition : Sendable, IJob
    {
        public JobSendComposition()
        {

        }

        public JobSendComposition(string name, Playlist playlist, Device device)
        {
            Name = name;
            Playlist = playlist;
            Device = device;
            //OSCSender = device.OSCSender;

            SendNextCommand = new RelayCommand(p => SendNext());
            SendPreviousCommand = new RelayCommand(p => SendPrevious());
        }

        //public OSCSender OSCSender { get; set; }

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

        public bool Pause { get; set; }

        int CompositionIndex = -1;

        public void Execute()
        {
            if(!Pause)
                SendNext();
        }

        public void Send()
        {
            if(CurrentComposition != null)
            {
                //OSCSender.QueueMessage("/CompositionReloaded", true);
                //OSCSender.QueueObject(CurrentComposition);
                //OSCSender.SendQueue();
            }
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

        public void SendNext()
        {
            Next();
            Send();
        }

        public void SendPrevious()
        {
            Previous();
            Send();
        }
    }
}