using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels
{
    public interface IAnimMode
    {
        double[] UpdateOnBeatTick(double[] doubleToAnimate, double period, Range range, Easing easing);
        double[] UpdateOnGameLoop(double[] doubleToAnimate, double period, Range range, Easing easing);
    }
}