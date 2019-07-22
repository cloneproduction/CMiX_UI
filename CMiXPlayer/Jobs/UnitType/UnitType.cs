using CMiX.MVVM.ViewModels;
using FluentScheduler;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiXPlayer.Jobs
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

            SelectedUnitType = new SecondUnit();

        }

        public TimeUnit TimeUnit { get; set; }

        public ObservableCollection<ViewModel> UnitTypes { get; set; }

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
