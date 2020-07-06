using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public enum ModeType
    {
        STEADY,
        LFO,
        RANDOM,
        STEPPER
    }

    public enum RandomType
    {
        JUMP,
        LINEAR
    }

    public enum SteadyType
    {
        LINEAR,
        RANDOM
    }
}
