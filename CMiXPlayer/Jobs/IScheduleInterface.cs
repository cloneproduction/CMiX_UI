using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;

namespace CMiXPlayer.Jobs
{
    public interface IScheduleInterface<T>
    {
        Action<T> SetScheduler { get; set; }
    }
}