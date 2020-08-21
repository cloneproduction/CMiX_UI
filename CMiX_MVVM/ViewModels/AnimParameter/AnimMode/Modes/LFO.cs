using System;
using System.Diagnostics;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class LFO : Sendable, IAnimMode
    {
        public LFO()
        {
            Stopwatch = new Stopwatch();
        }

        public LFO(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {

        }

        public void Update()
        {
            Console.WriteLine("LFO Update " + Stopwatch.ElapsedMilliseconds);
            if (!Stopwatch.IsRunning)
                Stopwatch.Start();
            else
                Reset();
        }

        public void Reset()
        {
            Stopwatch.Reset();
            Stopwatch.Start();
        }

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetAndNotify(ref _IsEnabled, value);
        }


        public Stopwatch Stopwatch { get; set; }

        private bool _invert;
        public bool Invert
        {
            get => _invert;
            set => SetAndNotify(ref _invert, value);
        }
    }
}