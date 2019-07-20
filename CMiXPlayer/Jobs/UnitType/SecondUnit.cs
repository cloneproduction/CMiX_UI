using CMiX.MVVM.ViewModels;
using System;
using FluentScheduler;

namespace CMiXPlayer.Jobs
{
    public class SecondUnit : ViewModel
    {
        public SecondUnit(TimeUnit timeunit)
        {
            Name = "Seconds";
            SecondUnitAction = new Action(() => { SetSecondUnit(timeunit); });
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public Action SecondUnitAction { get; set; }

        public void SetSecondUnit(TimeUnit timeunit)
        {
            timeunit.Seconds();
        }
    }
}