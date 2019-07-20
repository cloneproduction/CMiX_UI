using CMiX.MVVM.ViewModels;
using FluentScheduler;
using System.Collections.ObjectModel;

namespace CMiXPlayer.Jobs
{
    public class ToRunType : ViewModel
    {
        public ToRunType(Schedule schedule)
        {
            Schedule = schedule;
            Interval = 10;
            ToRunTypes = new ObservableCollection<ViewModel>();
            ToRunTypes.Add(new ToRunNow(Schedule));
            ToRunTypes.Add(new ToRunEvery(Schedule, Interval));
        }

        public Schedule Schedule { get; set; }

        private int _interval;
        public int Interval
        {
            get => _interval;
            set => SetAndNotify(ref _interval, value);
        }

        public ObservableCollection<ViewModel> ToRunTypes { get; set; }

        private ViewModel _selectedToRunType;
        public ViewModel SelectedToRunType
        {
            get => _selectedToRunType;
            set => SetAndNotify(ref _selectedToRunType, value);
        }
    }
}