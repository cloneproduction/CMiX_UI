using System;

namespace CMiX.MVVM.ViewModels
{
    public interface IAnimMode : IDisposable
    {
        void UpdateOnBeatTick(double[] doubleToAnimate, double period, Range range, Easing easing);
        void UpdateOnGameLoop(double[] doubleToAnimate, double period, Range range, Easing easing);
    }
}