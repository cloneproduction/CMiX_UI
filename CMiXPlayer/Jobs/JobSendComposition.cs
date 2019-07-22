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

        public JobSendComposition(Playlist playlist, OSCMessenger oscmessenger)
        {
            Playlist = playlist;
            OSCMessenger = oscmessenger;
        }

        public Playlist Playlist{ get; set; }
        public OSCMessenger OSCMessenger { get; set; }

        int CompositionIndex = 0;

        public void Execute()
        {
            foreach (var item in Playlist.Compositions)
            {
                Console.WriteLine(item.Name);
            }
            if (CompositionIndex <= Playlist.Compositions.Count - 1)
            {
                CompositionModel compositionmodel = Playlist.Compositions[CompositionIndex];
                OSCMessenger.SendMessage("/CompositionReloaded", true);
                OSCMessenger.QueueObject(compositionmodel);
                OSCMessenger.SendQueue();
                Console.WriteLine("The composition named : " + compositionmodel.Name + " has been sent to engine");

                CompositionIndex += 1;
            }
            else
            {
                CompositionIndex = 0;
            }
        }
    }
}