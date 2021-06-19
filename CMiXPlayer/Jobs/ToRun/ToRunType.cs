using CMiX.Core.Presentation.ViewModels;
using FluentScheduler;
using System.Collections.ObjectModel;

namespace CMiXPlayer.Jobs
{
    public class ToRunType : ViewModel
    {
        public ToRunType()
        {
            ToRunTypes = new ObservableCollection<ViewModel>();
            ToRunTypes.Add(new ToRunNow());
            ToRunTypes.Add(new ToRunEvery());
            ToRunTypes.Add(new ToRunNowAndEvery());

            SelectedToRunType = new ToRunEvery();
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