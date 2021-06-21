using System;
using FluentScheduler;
using CMiX.Core.Presentation.ViewModels;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class ToRunNowAndEvery : ViewModel, IScheduleInterface<Schedule>
    {
        public ToRunNowAndEvery()
        {
            Name = "ToRunNowAndEvery";
            SetScheduler = new Action<Schedule>((s) => { SetToRunNowAndEvery(s); });
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

        private void SetToRunNowAndEvery(Schedule schedule)
        {
            var unittype = (IScheduleInterface<TimeUnit>)UnitType.SelectedUnitType;
            unittype.SetScheduler.Invoke(schedule.ToRunNow().AndEvery(UnitType.UnitInterval.Interval));
        }
    }
}
