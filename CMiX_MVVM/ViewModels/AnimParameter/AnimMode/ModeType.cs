using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public enum ModeType
    {
        None,
        Steady,
        LFO,
        Random,
        Stepper
    }

    //public enum RandomType
    //{
    //    Jump,
    //    Linear
    //}

    public enum SteadyType
    {
        Linear,
        Random
    }
}
