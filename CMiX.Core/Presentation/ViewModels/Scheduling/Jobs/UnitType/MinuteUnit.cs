using System;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduling
{
    public class MinuteUnit : ViewModel, IUnit
    {
        public MinuteUnit()
        {
            Name = "Minutes";
            SetScheduler = new Action<TimeUnit>((s) => { SetUnit(s); });
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public Action<TimeUnit> SetScheduler { get; set; }

        public void SetUnit(TimeUnit timeUnit)
        {
            timeUnit.Minutes();
        }
    }
}
