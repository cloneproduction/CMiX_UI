using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.Controls
{
    public class BeatDisplay : Sendable
    {
        public BeatDisplay(AnimatedDouble animatedDouble)
        {
            AnimatedDouble = animatedDouble;
        }

        private AnimatedDouble _animatedDouble;
        public AnimatedDouble AnimatedDouble
        {
            get => _animatedDouble;
            set => SetAndNotify(ref _animatedDouble, value);
        }
    }
}
