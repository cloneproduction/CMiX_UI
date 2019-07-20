using CMiX.MVVM.ViewModels;
using System;

using FluentScheduler;

namespace CMiXPlayer.Jobs
{
    public class MinuteUnit : ViewModel
    {
        public MinuteUnit(TimeUnit timeunit)
        {
            Name = "Minutes";
            MinuteUnitAction = new Action(() => { SetMinuteUnit(timeunit); });
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public Action MinuteUnitAction { get; set; }

        public void SetMinuteUnit(TimeUnit timeunit)
        {
            timeunit.Minutes();
        }
    }
}