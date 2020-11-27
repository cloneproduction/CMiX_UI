using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels
{
    public interface IAnimMode
    {
        void UpdateOnBeatTick(double[] doubleToAnimate, double period, Range range, Easing easing);
        void UpdateOnGameLoop(double[] doubleToAnimate, double period, Range range, Easing easing);
    }
}