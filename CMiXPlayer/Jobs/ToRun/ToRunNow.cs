using CMiX.MVVM.ViewModels;
using FluentScheduler;
using System;

namespace CMiXPlayer.Jobs
{
    public class ToRunNow : ViewModel, IScheduleInterface
    {
        public ToRunNow(Schedule schedule)
        {
            Schedule = schedule;
            Name = "ToRunNow";
            Action = new Action(() => { SetToRunNow(); });
        }

        private Schedule _schedule;
        public Schedule Schedule
        {
            get => _schedule;
            set => SetAndNotify(ref _schedule, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public Action Action { get; set; }

        public void SetToRunNow()
        {
            Schedule.ToRunNow();
            Console.WriteLine("SetToRunNow");
        }
    }
}
