using System;
using FluentScheduler;

namespace CMiXPlayer.Jobs
{
    public class MyRegistry : Registry
    {
        public Action MyAction { get; set; }

        public Schedule MySchedule { get; set; }

        public MyRegistry()
        {
            MySchedule = new Schedule(MyAction);
        }

        //public void SelectTimeUnit(Schedule schedule, string timeUnitType, string unitType, int interval)
        //{
        //    if(timeUnitType == "ToRunEvery")
        //    {
        //        if(unitType == "Seconds")
        //        {
        //            SecondUnit(schedule.ToRunEvery(interval));
        //        }
        //        else if(unitType == "Minutes")
        //        {
        //            MinuteUnit(schedule.ToRunEvery(interval));
        //        }
        //        else if(unitType == "Hours")
        //        {
        //            HourUnit(schedule.ToRunEvery(interval));
        //        }
        //    }
        //    else if (timeUnitType == "ToRunOnceIn")
        //    {
        //        MySchedule.ToRunOnceIn(interval);
        //    }
        //}

        //public SecondUnit SecondUnit(TimeUnit timeUnit)
        //{
        //    return timeUnit.Seconds();
        //}

        //public HourUnit HourUnit(TimeUnit timeUnit)
        //{
        //    return timeUnit.Hours();
        //}

        //public MinuteUnit MinuteUnit(TimeUnit timeUnit)
        //{
        //    return timeUnit.Minutes();
        //}
    }
}
