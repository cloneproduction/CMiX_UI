using System;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduling
{
    public class ToRunEvery : ViewModel, IToRun//, IScheduleInterface<Schedule>
    {
        public ToRunEvery()
        {
            Name = "ToRunEvery";
            SetScheduler = new Action<Schedule>((s) => { SetSchedule(s); });
            UnitType = new UnitType();
            UnitInterval = new UnitInterval(5);
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

        //public void SetToRunEvery(Schedule schedule)
        //{
        //    var unittype = (IScheduleInterface<TimeUnit>)UnitType.SelectedUnitType;
        //    unittype.SetScheduler.Invoke(schedule.ToRunEvery(UnitType.UnitInterval.Interval));
        //}

        private void SetSchedule(Schedule schedule)
        {
            UnitType.SetScheduler.Invoke(schedule.ToRunEvery(UnitType.UnitInterval.Interval));
            //schedule.ToRunEvery(UnitType.UnitInterval.Interval);
        }
    }
}
