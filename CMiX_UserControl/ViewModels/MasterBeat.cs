using CMiX.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class MasterBeat : ViewModel
    {
        public MasterBeat(IMessenger messenger)
            : this(
                  messenger: messenger,
                  period: 0.0,
                  multiplier: 1)
        { }

        public MasterBeat(IMessenger messenger, double period, int multiplier)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            Period = period;
            Multiplier = multiplier;

            ResetCommand = new RelayCommand(p => Reset());
            ResyncCommand = new RelayCommand(p => Resync());
            MultiplyCommand = new RelayCommand(p => Multiply());
            DivideCommand = new RelayCommand(p => Divide());
            TapCommand = new RelayCommand(p => Tap());

            tapPeriods = new List<double>();
            tapTime = new List<double>();
        }

        private double _period;
        public double Period
        {
            get => _period;
            set => SetAndNotify(ref _period, value);
        }

        private int _multiplier;
        public int Multiplier
        {
            get => _multiplier;
            set => SetAndNotify(ref _multiplier, value);
        }

        public ICommand ResetCommand { get; }

        public ICommand ResyncCommand { get; }

        public ICommand MultiplyCommand { get; }

        public ICommand DivideCommand { get; }

        public ICommand TapCommand { get; }

        public IMessenger Messenger { get; }

        private readonly List<double> tapPeriods;
        private readonly List<double> tapTime;

        private string Address => "/MasterPeriod";

        private void Reset()
        {
            Multiplier = 1;
        }

        private void Resync()
        {
            Messenger.SendMessage(Address, CurrentTime);
        }

        private void Multiply()
        {
            Period /= 2.0;

            Messenger.SendMessage(Address, Period);
        }

        private void Divide()
        {
            Period *= 2.0;

            Messenger.SendMessage(Address, Period);
        }

        private void Tap()
        {
            Period = GetMasterPeriod();

            Messenger.SendMessage(Address, Period);
        }

        private double GetMasterPeriod()
        {
            double ms = CurrentTime;

            if (tapTime.Count > 1 && ms - tapTime[tapTime.Count - 1] > 5000)
            {
                tapTime.Clear();
            }
            tapTime.Add(ms);

            if (tapTime.Count > 1)
            {
                tapPeriods.Clear();
                for (int i = 1; i < tapTime.Count; i++)
                {
                    double average = tapTime[i] - tapTime[i - 1];
                    tapPeriods.Add(average);
                }
            }
            return tapPeriods.Sum() / tapPeriods.Count;
        }

        private double CurrentTime => (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
    }
}
