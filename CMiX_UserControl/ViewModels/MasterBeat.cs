using CMiX.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class MasterBeat : Beat
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

            ResyncCommand = new RelayCommand(p => Resync());
            TapCommand = new RelayCommand(p => Tap());

            tapPeriods = new List<double>();
            tapTime = new List<double>();
        }

        private double _period;
        public override double Period
        {
            get => _period;
            set
            {
                SetAndNotify(ref _period, value);
                OnPeriodChanged(Period);
                Notify(nameof(BPM));
            }
        }

        public ICommand ResyncCommand { get; }

        public ICommand TapCommand { get; }

        private IMessenger Messenger { get; }

        private readonly List<double> tapPeriods;
        private readonly List<double> tapTime;

        private string Address => "/MasterPeriod";

        private void Resync()
        {
            Messenger.SendMessage(Address + nameof(Resync), CurrentTime + Period);
        }

        protected override void Multiply()
        {
            Period /= 2.0;

            Messenger.SendMessage(Address, Period);
        }

        protected override void Divide()
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
