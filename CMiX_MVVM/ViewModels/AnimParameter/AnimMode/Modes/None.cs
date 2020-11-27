using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels
{
    public class None : ViewModel, IAnimMode
    {
        public None(string name, IColleague parentSender)
        {

        }

        public void UpdateOnBeatTick(double[] doubleToAnimate, double period, Range range, Easing easing)
        {

        }

        public void UpdateOnGameLoop(double[] doubleToAnimate, double period, Range range, Easing easing)
        {
            for (int i = 0; i < doubleToAnimate.Length; i++)
            {
                doubleToAnimate[i] = 0;
            }
        }

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetAndNotify(ref _IsEnabled, value);
        }
    }
}