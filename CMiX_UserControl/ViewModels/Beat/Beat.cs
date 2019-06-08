using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.Services;
using Memento;

namespace CMiX.ViewModels
{
    public abstract class Beat : ViewModel
    {
        #region CONSTRUCTOR
        public Beat(ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor)
            : base(oscmessengers, mementor)
        {
            ResetCommand = new RelayCommand(p => Reset());
            MultiplyCommand = new RelayCommand(p => Multiply());
            DivideCommand = new RelayCommand(p => Divide());
        }
        #endregion

        #region PROPERTIES
        public ICommand ResetCommand { get; }
        public ICommand MultiplyCommand { get; }
        public ICommand DivideCommand { get; }



        public abstract double Period { get; set; }

        public double BPM
        {
            get
            {
                var bpm = 60000 / Period;
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


        #endregion

        #region EVENTS
        public delegate void PeriodChangedEventHandler(Beat sender, double newValue);

        public event PeriodChangedEventHandler PeriodChanged;

        protected void OnPeriodChanged(double newPeriod) => PeriodChanged?.Invoke(this, newPeriod);
        #endregion
    }
}