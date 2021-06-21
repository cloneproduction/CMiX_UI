﻿using System;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class SecondUnit : ViewModel, IScheduleInterface<TimeUnit>
    {
        public SecondUnit()
        {
            Name = "Seconds";
            SetScheduler = new Action<TimeUnit>((s) => { SetSecondUnit(s); });
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public Action<TimeUnit> SetScheduler { get; set; }

        public void SetSecondUnit(TimeUnit timeunit)
        {
            timeunit.Seconds();
        }
    }
}
