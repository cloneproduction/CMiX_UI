using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.MVVM.ViewModels;
using FluentScheduler;

namespace CMiXPlayer.Jobs
{
    public class TimeUnitTypeViewModel : ViewModel
    {
        public TimeUnitTypeViewModel()
        {
            TimeUnitTypeAction = new List<Action>();
            TimeUnitTypeAction.Add(new Action(() => { ToRunEvery(); }));
            TimeUnitTypeAction.Add(new Action(() => { ToRunOnceIn(5); }));
        }

        private Schedule _schedule;
        public Schedule Schedule
        {
            get => _schedule;
            set => SetAndNotify(ref _schedule, value);
        }

        private List<Action> timeunittypeaction;
        public List<Action> TimeUnitTypeAction
        {
            get { return timeunittypeaction; }
            set { timeunittypeaction = value; }
        }

        public void ToRunEvery()
        {
            Schedule.ToRunNow();
        }

        public void ToRunOnceIn(int interval)
        {
            Schedule.ToRunEvery(interval);
        }
    }
}
