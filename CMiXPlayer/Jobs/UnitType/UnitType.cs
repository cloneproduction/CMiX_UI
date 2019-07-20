using CMiX.MVVM.ViewModels;
using FluentScheduler;
using System.Collections.ObjectModel;

namespace CMiXPlayer.Jobs
{
    public class UnitType : ViewModel
    {
        public UnitType(TimeUnit timeunit)
        {
            TimeUnit = timeunit;
            UnitTypes = new ObservableCollection<ViewModel>();
            UnitTypes.Add(new SecondUnit(TimeUnit));
            UnitTypes.Add(new MinuteUnit(TimeUnit));
            UnitTypes.Add(new HourUnit(TimeUnit));
            SelectedUnitType = new SecondUnit(TimeUnit);
        }

        public TimeUnit TimeUnit { get; set; }

        public ObservableCollection<ViewModel> UnitTypes { get; set; }

        private ViewModel _selectedUnitType;
        public ViewModel SelectedUnitType
        {
            get => _selectedUnitType;
            set => SetAndNotify(ref _selectedUnitType, value);
        }
    }
}
