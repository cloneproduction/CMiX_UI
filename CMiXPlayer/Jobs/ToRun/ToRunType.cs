using CMiX.MVVM.ViewModels;
using FluentScheduler;
using System.Collections.ObjectModel;

namespace CMiXPlayer.Jobs
{
    public class ToRunType : ViewModel//, IScheduleInterface<Schedule, TimeUnit>
    {
        public ToRunType()
        {
            Interval = 10;

            ToRunTypes = new ObservableCollection<ViewModel>();
            ToRunTypes.Add(new ToRunNow());
            ToRunTypes.Add(new ToRunEvery(Interval));

            SelectedToRunType = new ToRunNow();
        }

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

        public void SetRunType(Schedule schedule)
        {
            var selected = SelectedToRunType as IScheduleInterface<Schedule>;
            selected.SetScheduler.Invoke(schedule);
        }
    }
}