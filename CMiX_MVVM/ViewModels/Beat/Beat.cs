using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;
using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Beat : Sendable
    {
        public Beat()
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
            BeatTick = 0;

            CompositionTarget.Rendering += CompositionTarget_Rendering;


            ResetCommand = new RelayCommand(p => Reset());
            MultiplyCommand = new RelayCommand(p => Multiply());
            DivideCommand = new RelayCommand(p => Divide());
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as BeatModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public ICommand ResetCommand { get; }
        public ICommand MultiplyCommand { get; }
        public ICommand DivideCommand { get; }

        public abstract double Period { get; set; }

        private double _bpm;
        public double BPM
        {
            get
            {
                _bpm = 60000 / Period;
                if (double.IsInfinity(_bpm) || double.IsNaN(_bpm))
                    return 0;
                else
                    return _bpm;
            }
            set
            {
                Period = 60000 / value;
                SetAndNotify(ref _bpm, value);
            }
        }



        private double _multiplier;
        public virtual double Multiplier
        {
            get => _multiplier;
            set =>  SetAndNotify(ref _multiplier, value);
        }

        private void Reset() => Multiplier = 1;

        protected abstract void Multiply();
        protected abstract void Divide();

        public delegate void PeriodChangedEventHandler(Beat sender, double newValue);

        public event PeriodChangedEventHandler PeriodChanged;

        protected void OnPeriodChanged(double newPeriod) => PeriodChanged?.Invoke(this, newPeriod);


        public event EventHandler BeatTap;
        public void OnBeatTap()
        {
            EventHandler handler = BeatTap;
            if (null != handler) handler(this, EventArgs.Empty);
        }

        public event EventHandler StopWatchReset;
        public void OnStopWatchReset()
        {
            EventHandler handler = StopWatchReset;
            if (null != handler) handler(this, EventArgs.Empty);
        }

        public Stopwatch Stopwatch { get; set; }

        public void ResetStopWatch()
        {
            Stopwatch.Reset();
            Stopwatch.Start();
        }


        private double _progress = 0;
        public double Progress
        {
            get => _progress ; 
            set => SetAndNotify(ref _progress, value);
        }



        public virtual void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            //Console.WriteLine("Stopwatch.ElapsedMilliseconds " + Stopwatch.ElapsedMilliseconds);
            //Console.WriteLine("NormalizedTime " + NormalizedTime);

            if (Stopwatch.ElapsedMilliseconds > Math.Floor(Period))
            {
                BeatTick++;
                if (BeatTick >= 4)
                    BeatTick = 0;
                ResetStopWatch();
                OnBeatTap();
            }
        }

        private int _beatTick;
        public int BeatTick
        {
            get => _beatTick;
            set => SetAndNotify(ref _beatTick, value);
        }
    }
}