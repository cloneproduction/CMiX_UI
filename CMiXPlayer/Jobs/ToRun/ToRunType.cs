using CMiX.MVVM.ViewModels;
using FluentScheduler;
using System;
using System.Collections.ObjectModel;

namespace CMiXPlayer.Jobs
{
    public class ToRunType : ViewModel, IScheduleInterface
    {
        public ToRunType(Schedule schedule)
        {
            Schedule = schedule;
            Interval = 10;
            Action = new Action(() => { SetUnitType(); });
            ToRunTypes = new ObservableCollection<ViewModel>();
            ToRunTypes.Add(new ToRunNow(Schedule));
            ToRunTypes.Add(new ToRunEvery(Schedule, Interval));
            SelectedToRunType = new ToRunNow(Schedule);
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

        public Action Action { get; set; }

        public void SetUnitType()
        {
            Console.WriteLine("SetUnitType ToRunNow");
            var selected = (IScheduleInterface)SelectedToRunType;
            selected.Action.Invoke();
        }
    }
}