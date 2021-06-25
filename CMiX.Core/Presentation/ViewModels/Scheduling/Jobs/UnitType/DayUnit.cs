using System;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduling
{
    public class DayUnit : ViewModel, IUnit//, IScheduleInterface<TimeUnit>
    {
        public DayUnit()
        {
            Name = "Days";
            SetScheduler = new Action<TimeUnit>((s) => { SetUnit(s); });
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


        public void SetUnit(TimeUnit timeUnit)
        {
            timeUnit.Days().At(At.Hours, At.Minutes);
        }
    }
}
