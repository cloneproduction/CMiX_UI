using System;

namespace CMiX.MVVM.ViewModels
{
    public interface IAnimMode : ISenderTest, IDisposable
    {
        void UpdateOnBeatTick(double[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier);
        void UpdateOnGameLoop(double[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier);
    }
}