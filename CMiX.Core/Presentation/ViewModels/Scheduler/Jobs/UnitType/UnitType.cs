using System;
using System.Collections.ObjectModel;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class UnitType : ViewModel, IScheduleInterface<TimeUnit>
    {
        public UnitType()
        {
            SetScheduler = new Action<TimeUnit>((s) => { SetUnitType(s); });

            UnitTypes = new ObservableCollection<ViewModel>();

            UnitTypes.Add(new SecondUnit());
            UnitTypes.Add(new MinuteUnit());
            UnitTypes.Add(new HourUnit());
            UnitTypes.Add(new DayUnit());
            UnitTypes.Add(new MonthUnit());

            SelectedUnitType = new SecondUnit();
            UnitInterval = new UnitInterval(60);
        }

        public TimeUnit TimeUnit { get; set; }

        public ObservableCollection<ViewModel> UnitTypes { get; set; }

        private UnitInterval _unitinterval;
        public UnitInterval UnitInterval
        {
            get => _unitinterval;
            set => SetAndNotify(ref _unitinterval, value);
        }

        private ViewModel _selectedUnitType;
        public ViewModel SelectedUnitType
        {
            get => _selectedUnitType;
            set => SetAndNotify(ref _selectedUnitType, value);
        }

        public Action<TimeUnit> SetScheduler { get; set; }

        public void SetUnitType(TimeUnit timeunit)
        {
            var selected = SelectedUnitType as IScheduleInterface<TimeUnit>;
            selected.SetScheduler.Invoke(timeunit);
        }
    }
}
