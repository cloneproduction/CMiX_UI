using CMiX.MVVM.ViewModels;
using System;
using FluentScheduler;

namespace CMiXPlayer.Jobs
{
    public class HourUnit : ViewModel
    {
        public HourUnit(TimeUnit timeunit)
        {
            Name = "Hours";
            HourUnitAction = new Action(() => { SetHourUnit(timeunit); });
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public Action HourUnitAction { get; set; }

        public void SetHourUnit(TimeUnit timeunit)
        {
            timeunit.Hours();
        }
    }
}