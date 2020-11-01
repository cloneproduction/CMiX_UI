using CMiX.MVVM.ViewModels;
using System;
using FluentScheduler;

namespace CMiXPlayer.Jobs
{
    public class HourUnit : Sendable, IScheduleInterface<TimeUnit>
    {
        public HourUnit()
        {
            Name = "Hours";
            SetScheduler = new Action<TimeUnit>((s) => { SetHourUnit(s); });
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public Action<TimeUnit> SetScheduler { get; set; }

        public void SetHourUnit(TimeUnit timeunit)
        {
            timeunit.Hours();
        }
    }
}