using CMiX.MVVM.ViewModels;
using FluentScheduler;
using System;

namespace CMiXPlayer.Jobs
{
    public class ToRunNow : ViewModel, IScheduleInterface<Schedule>
    {
        public ToRunNow()
        {
            Name = "ToRunNow";
            SetScheduler = new Action<Schedule>((s) => { SetToRunNow(s); });
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public Action<Schedule> SetScheduler { get; set; }

        public void SetToRunNow(Schedule schedule)
        {
            schedule.ToRunNow();
        }
    }
}
