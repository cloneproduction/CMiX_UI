using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public abstract class Beat : ViewModel
    {
        public Beat()
        {
            ResetCommand = new RelayCommand(p => Reset());
            MultiplyCommand = new RelayCommand(p => Multiply());
            DivideCommand = new RelayCommand(p => Divide());
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
            set => SetAndNotify(ref _multiplier, value);
        }

        private void Reset() => Multiplier = 1;

        protected abstract void Multiply();
        protected abstract void Divide();

        public delegate void PeriodChangedEventHandler(Beat sender, double newValue);

        public event PeriodChangedEventHandler PeriodChanged;

        protected void OnPeriodChanged(double newPeriod) => PeriodChanged?.Invoke(this, newPeriod);
    }
}