using CMiX.MVVM.ViewModels;
using System;
using FluentScheduler;

namespace CMiXPlayer.Jobs
{
    public class DayUnit : Sendable, IScheduleInterface<TimeUnit>
    {
        public DayUnit()
        {
            Name = "Days";
            SetScheduler = new Action<TimeUnit>((s) => { SetDayUnit(s); });
            At = new At();
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private int _hours;
        public int Hours
        {
            get => _hours;
            set => SetAndNotify(ref _hours, value);
        }

        private int _minutes;
        public int Minutes
        {
            get => _minutes;
            set => SetAndNotify(ref _minutes, value);
        }

        public At At { get; set; }

        public Action<TimeUnit> SetScheduler { get; set; }

        public void SetDayUnit(TimeUnit timeunit)
        {
            timeunit.Days().At(At.Hours, At.Minutes);
        }
    }
}