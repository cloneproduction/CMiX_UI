using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiXPlayer.Jobs
{
    public interface IScheduleInterface
    {
        Action Action { get; set; }
    }
}
