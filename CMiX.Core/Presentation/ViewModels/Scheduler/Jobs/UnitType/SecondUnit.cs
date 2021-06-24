using System;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class SecondUnit : ViewModel, IUnit// IScheduleInterface<TimeUnit>
    {
        public SecondUnit()
        {
            Name = "Seconds";
            SetScheduler = new Action<TimeUnit>((s) => { SetUnit(s); });
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }
        public Action<TimeUnit> SetScheduler { get; set; }


        private void SetUnit(TimeUnit timeunit)
        {
            //SetScheduler.Invoke(timeunit);
            timeunit.Seconds();
        }
    }
}
