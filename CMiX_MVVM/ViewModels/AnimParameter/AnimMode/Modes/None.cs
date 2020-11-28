using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class None : ViewModel, IAnimMode
    {
        public None(string name, IColleague parentSender)
        {

        }

        public double[] UpdateOnBeatTick(double[] doubleToAnimate, double period, Range range, Easing easing)
        {
            return doubleToAnimate;
        }

        public double[] UpdateOnGameLoop(double[] doubleToAnimate, double period, Range range, Easing easing)
        {
            return doubleToAnimate;
        }

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetAndNotify(ref _IsEnabled, value);
        }
    }
}