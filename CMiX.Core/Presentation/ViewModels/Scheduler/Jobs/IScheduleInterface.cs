using System;

namespace CMiX.Core.Presentation.ViewModels.Scheduler
{
    public interface IScheduleInterface<T>
    {
        Action<T> SetScheduler { get; set; }
    }
}
