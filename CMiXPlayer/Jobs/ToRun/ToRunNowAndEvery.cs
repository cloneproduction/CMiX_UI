using System;
using FluentScheduler;
using CMiX.MVVM.ViewModels;

namespace CMiXPlayer.Jobs
{
    public class ToRunNowAndEvery : Sendable, IScheduleInterface<Schedule>
    {
        #region CONSTRUCTORS
        public ToRunNowAndEvery()
        {
            Name = "ToRunNowAndEvery";
            SetScheduler = new Action<Schedule>((s) => { SetToRunNowAndEvery(s); });
            UnitType = new UnitType();
            UnitInterval = new UnitInterval(60);
        }
        #endregion

        #region PROPERTIES
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
        #endregion

        #region PRIVATE METHODS
        private void SetToRunNowAndEvery(Schedule schedule)
        {
            var unittype = (IScheduleInterface<TimeUnit>)UnitType.SelectedUnitType;
            unittype.SetScheduler.Invoke(schedule.ToRunNow().AndEvery(UnitType.UnitInterval.Interval));
        }
        #endregion
    }
}