using CMiX.MVVM.ViewModels;
using FluentScheduler;
using System;

namespace CMiXPlayer.Jobs
{
    public class ToRunNow : ViewModel
    {
        public ToRunNow(Schedule schedule)
        {
            Name = "ToRunNow";
            Action = new Action(() => { SetToRunNow(schedule); });
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public Action Action { get; set; }

        public void SetToRunNow(Schedule schedule)
        {
            schedule.ToRunNow();
        }
    }
}
