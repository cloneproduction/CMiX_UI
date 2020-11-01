using CMiX.MVVM.ViewModels;
using FluentScheduler;
using System.Collections.ObjectModel;

namespace CMiXPlayer.Jobs
{
    public class ToRunType : Sendable
    {
        public ToRunType()
        {
            ToRunTypes = new ObservableCollection<Sendable>();
            ToRunTypes.Add(new ToRunNow());
            ToRunTypes.Add(new ToRunEvery());
            ToRunTypes.Add(new ToRunNowAndEvery());

            SelectedToRunType = new ToRunEvery();
        }

        public ObservableCollection<Sendable> ToRunTypes { get; set; }

        private Sendable _selectedToRunType;
        public Sendable SelectedToRunType
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