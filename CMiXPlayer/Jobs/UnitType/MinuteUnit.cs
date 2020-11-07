using CMiX.MVVM.ViewModels;
using System;
using FluentScheduler;

namespace CMiXPlayer.Jobs
{
    public class MinuteUnit : ViewModel, IScheduleInterface<TimeUnit>
    {
        public MinuteUnit()
        {
            Name = "Minutes";
            SetScheduler = new Action<TimeUnit>((s) => { SetMinuteUnit(s); });
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public Action <TimeUnit> SetScheduler { get; set; }

        public void SetMinuteUnit(TimeUnit timeunit)
        {
            timeunit.Minutes();
        }
    }
}