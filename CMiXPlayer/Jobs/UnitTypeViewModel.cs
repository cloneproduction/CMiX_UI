using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace CMiXPlayer.Jobs
{
    public class UnitTypeViewModel : ViewModel
    {
        public UnitTypeViewModel(TimeUnit timeunit)
        {
            TimeUnit = timeunit;

            UnitTypeActions = new ObservableCollection<Action>();
            UnitTypeActions.Add(new Action(() => { SecondUnit(TimeUnit); }));
            UnitTypeActions.Add(new Action(() => { MinuteUnit(TimeUnit); }));
            UnitTypeActions.Add(new Action(() => { HourUnit(TimeUnit); }));
        }

        public ObservableCollection<Action> UnitTypeActions { get; set; }

        private TimeUnit _timeunit;
        public TimeUnit TimeUnit
        {
            get => _timeunit;
            set => SetAndNotify(ref _timeunit, value);
        }

        public void SecondUnit(TimeUnit timeunit)
        {
            timeunit.Seconds();
        }

        public void MinuteUnit(TimeUnit timeunit)
        {
            timeunit.Minutes();
        }

        public void HourUnit(TimeUnit timeunit)
        {
            timeunit.Hours();
        }

        public void DaysUnit(TimeUnit timeunit)
        {
            timeunit.Days();
        }

        public void WeekUnit(TimeUnit timeunit)
        {
            timeunit.Weeks();
        }

        public void WeekDayUnit(TimeUnit timeunit)
        {
            timeunit.Weekdays();
        }

        public void MonthUnit(TimeUnit timeunit)
        {
            timeunit.Months();
        }

        public void YearUnit(TimeUnit timeunit)
        {
            timeunit.Years();
        }
    }
}
