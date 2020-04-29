using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class MasterBeat : Beat
    {
        #region CONSTRUCTORS
        public MasterBeat(MessengerService messengerService)
        : this
        (
            messengerService: messengerService,
            period: 0.0,
            multiplier: 1
        )
        { }

        public MasterBeat
            (
                MessengerService messengerService,
                double period,
                double multiplier
            )
        {
            MessengerService = messengerService;
            Period = period;
            Multiplier = multiplier;
            ResyncCommand = new RelayCommand(p => Resync());
            TapCommand = new RelayCommand(p => Tap());
            MessageAddress = "/MasterBeat/";
            tapPeriods = new List<double>();
            tapTime = new List<double>();
        }
        #endregion

        #region PROPERTIES
        public ICommand ResyncCommand { get; }
        public ICommand TapCommand { get; }

        private readonly List<double> tapPeriods;
        private readonly List<double> tapTime;

        private double CurrentTime => (DateTime.Now - DateTime.MinValue).TotalMilliseconds;

        private double _period;
        public override double Period
        {
            get => _period;
            set
            {
                SetAndNotify(ref _period, value);
                OnPeriodChanged(Period);
                Notify(nameof(BPM));
                //SendMessages(MessageAddress + nameof(Period), Period);
            }
        }

        public string MessageAddress { get; set; }
        public MessengerService MessengerService { get; set; }
        #endregion

        #region METHODS
        private void Resync()
        {
            //Sender.SendMessages(MessageAddress + nameof(Resync), CurrentTime + Period);
        }

        protected override void Multiply()
        {
            Period /= 2.0;
        }

        protected override void Divide()
        {
            Period *= 2.0;
        }

        private void Tap()
        {
            Period = GetMasterPeriod();
        }

        private double GetMasterPeriod()
        {
            double ms = CurrentTime;

            if (tapTime.Count > 1 && ms - tapTime[tapTime.Count - 1] > 5000)
                tapTime.Clear();

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
        #endregion
    }
}