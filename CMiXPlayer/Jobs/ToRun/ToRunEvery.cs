using CMiX.Core.Presentation.ViewModels;
using FluentScheduler;
using System;
using System.Windows.Input;

namespace CMiXPlayer.Jobs
{
    public class ToRunEvery : ViewModel, IScheduleInterface<Schedule>
    {
        public ToRunEvery()
        {
            Name = "ToRunEvery";
            SetScheduler = new Action<Schedule>((s) => { SetToRunEvery(s); });
            UnitType = new UnitType();
            UnitInterval = new UnitInterval(60);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private UnitInterval _unitinterval;
        public UnitInterval UnitInterval
        {
            get => _unitinterval;
            set => SetAndNotify(ref _unitinterval, value);
        }

        private UnitType _unittype;
        public UnitType UnitType
        {
            get => _unittype;
            set => SetAndNotify(ref _unittype, value);
        }

        public Action<Schedule> SetScheduler { get; set; }

        public void SetToRunEvery(Schedule schedule)
        {
            var unittype = (IScheduleInterface<TimeUnit>)UnitType.SelectedUnitType;
            unittype.SetScheduler.Invoke(schedule.ToRunEvery(UnitType.UnitInterval.Interval));
        }
    }
}