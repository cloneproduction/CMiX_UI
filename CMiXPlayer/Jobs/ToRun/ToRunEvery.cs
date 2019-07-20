using CMiX.MVVM.ViewModels;
using FluentScheduler;
using System;

namespace CMiXPlayer.Jobs
{
    public class ToRunEvery : ViewModel
    {
        public ToRunEvery(Schedule schedule, int interval)
        {
            Name = "ToRunEvery";
            Action = new Action(() => { SetToRunEvery(schedule, interval); });
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public Action Action { get; set; }

        public void SetToRunEvery(Schedule schedule, int interval)
        {
            schedule.ToRunEvery(interval);
        }
    }
}
