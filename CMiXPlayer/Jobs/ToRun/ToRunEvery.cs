using CMiX.MVVM.ViewModels;
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

        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private UnitType _unittype;
        public UnitType UnitType
        {
            get => _unittype;
            set => SetAndNotify(ref _unittype, value);
        }

        private UnitType _selectedunittype;
        public UnitType SelectedUnitType
        {
            get => _selectedunittype;
            set => SetAndNotify(ref _selectedunittype, value);
        }

        public Action<Schedule> SetScheduler { get; set; }

        public void SetToRunEvery(Schedule schedule)
        {
            var unittype = (IScheduleInterface<TimeUnit>)UnitType.SelectedUnitType;
            unittype.SetScheduler.Invoke(schedule.ToRunEvery(SelectedUnitType.UnitInterval.Interval));
        }
    }
}