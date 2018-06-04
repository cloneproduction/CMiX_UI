using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class MainBeat : ViewModel
    {
        public MainBeat()
            : this(period: 0.0, multiplier: 1)
        { }

        public MainBeat(double period, int multiplier)
        {
            Period = period;
            Multiplier = multiplier;

            ResetCommand = new RelayCommand(p => Multiplier = 1);
        }

        private double _period = 0.0;
        public double Period
        {
            get => _period;
            set => SetAndNotify(ref _period, value);
        }

        private int _multiplier = 1;
        public int Multiplier
        {
            get => _multiplier;
            set => SetAndNotify(ref _multiplier, value);
        }

        public ICommand ResetCommand { get; }
    }
}
