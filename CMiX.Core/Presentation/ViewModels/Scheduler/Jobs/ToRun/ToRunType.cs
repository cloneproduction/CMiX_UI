using System;
using System.Collections.ObjectModel;
using FluentScheduler;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public class ToRunType : ViewModel
    {
        public ToRunType()
        {
            ToRunTypes = new ObservableCollection<IToRun>();
            ToRunTypes.Add(new ToRunNow());
            ToRunTypes.Add(new ToRunEvery());
            ToRunTypes.Add(new ToRunNowAndEvery());

            SelectedToRunType = new ToRunEvery();
        }

        public ObservableCollection<IToRun> ToRunTypes { get; set; }

        private IToRun _selectedToRunType;
        public IToRun SelectedToRunType
        {
            get => _selectedToRunType;
            set
            {
                if (SelectedToRunType != null)
                    SetScheduler = SelectedToRunType.SetScheduler;
                SetAndNotify(ref _selectedToRunType, value);
            }
        }

        public Action<Schedule> SetScheduler { get; set; }

        public void SetRunType(Schedule schedule)
        {
            //var selected = SelectedToRunType as IScheduleInterface<Schedule>;
            SelectedToRunType.SetScheduler.Invoke(schedule);
        }
    }
}
