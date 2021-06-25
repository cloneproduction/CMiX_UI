using System.Windows.Input;

namespace CMiX.Core.Presentation.ViewModels.Scheduling
{
    public class UnitInterval : ViewModel
    {
        public UnitInterval(int interval)
        {
            Interval = interval;
            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
        }

        public ICommand AddCommand { get; set; }
        public ICommand SubCommand { get; set; }

        private int _interval;
        public int Interval
        {
            get => _interval;
            set => SetAndNotify(ref _interval, value);
        }

        private void Add()
        {
            Interval += 1;
        }

        private void Sub()
        {
            if (Interval > 1)
                Interval -= 1;
        }
    }
}
