using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.Services;

namespace CMiX.ViewModels
{
    [Serializable]
    public abstract class Beat : ViewModel
    {
        public Beat(ObservableCollection<OSCMessenger> messengers) 
        {
            ResetCommand = new RelayCommand(p => Reset());
            MultiplyCommand = new RelayCommand(p => Multiply());
            DivideCommand = new RelayCommand(p => Divide());
        }

        public delegate void PeriodChangedEventHandler(Beat sender, double newValue);

        public event PeriodChangedEventHandler PeriodChanged;

        protected void OnPeriodChanged(double newPeriod) => PeriodChanged?.Invoke(this, newPeriod);

        public abstract double Period { get; set; }

        public double BPM
        {
            get
            {
                var bpm = 50000 / Period;

                if (double.IsInfinity(bpm) || double.IsNaN(bpm))
                    return 0;
                else
                    return bpm;
            }
        }

        private double _multiplier;
        public virtual double Multiplier
        {
            get => _multiplier;
            set => SetAndNotify(ref _multiplier, value);
        }

        private void Reset() => Multiplier = 1;

        protected abstract void Multiply();
        protected abstract void Divide();

        public ICommand ResetCommand { get; }
        public ICommand MultiplyCommand { get; }
        public ICommand DivideCommand { get; }
    }
}