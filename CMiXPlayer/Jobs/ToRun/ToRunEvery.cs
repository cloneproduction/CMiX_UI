using CMiX.MVVM.ViewModels;
using FluentScheduler;
using System;

namespace CMiXPlayer.Jobs
{
    public class ToRunEvery : ViewModel, IScheduleInterface<Schedule>
    {
        public ToRunEvery(int interval)
        {
            Name = "ToRunEvery";
            Interval = interval;
            SetScheduler = new Action<Schedule>((s) => { SetToRunEvery(s); });
            UnitType = new UnitType();
        }

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
            unittype.SetScheduler.Invoke(schedule.ToRunEvery(10));
        }
    }
}
