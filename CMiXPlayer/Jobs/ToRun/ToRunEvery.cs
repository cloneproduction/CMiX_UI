using CMiX.MVVM.ViewModels;
using FluentScheduler;
using System;
using System.Windows.Input;

namespace CMiXPlayer.Jobs
{
    public class ToRunEvery : ViewModel, IScheduleInterface<Schedule>
    {
        public ToRunEvery()
        {
            Name = "ToRunEvery";
            Interval = 60;
            SetScheduler = new Action<Schedule>((s) => { SetToRunEvery(s); });
            UnitType = new UnitType();

            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
        }

        public ICommand AddCommand { get; set; }
        public ICommand SubCommand { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private int _interval;
        public int Interval
        {
            get => _interval;
            set => SetAndNotify(ref _interval, value);
        }

        private UnitType _unittype;
        public UnitType UnitType
        {
            get => _unittype;
            set => SetAndNotify(ref _unittype, value);
        }
        public Action<Schedule> SetScheduler { get; set; }

        public void SetToRunEvery(Schedule schedule)
        {
            var unittype = (IScheduleInterface<TimeUnit>)UnitType.SelectedUnitType;
            unittype.SetScheduler.Invoke(schedule.ToRunEvery(Interval));
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
