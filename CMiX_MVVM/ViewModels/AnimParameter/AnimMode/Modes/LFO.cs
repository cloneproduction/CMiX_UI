using CMiX.MVVM.Resources;

namespace CMiX.MVVM.ViewModels
{
    public class LFO : ViewModel, IAnimMode
    {
        public LFO()
        {

        }


        public double[] UpdateOnBeatTick(double[] doubleToAnimate, double period, Range range, Easing easing)
        {
            return doubleToAnimate;
        }

        public double[] UpdateOnGameLoop(double[] doubleToAnimate, double period, Range range, Easing easing)
        {
            double periodOffset = 1.0 / doubleToAnimate.Length;
            double offset = 0.0;
            double val = 0.0;

            for (int i = 0; i < doubleToAnimate.Length; i++)
            {
                val = period + offset;
                if (val < 0.0)
                    val = 1.0 - (0.0 - val) % (1.0 - 0.0);
                else
                    val = 0.0 + (val - 0.0) % (1.0 - 0.0);

                doubleToAnimate[i] = Easings.Interpolate((float)Utils.Map(val, 0.0, 1.0, range.Minimum, range.Maximum), easing.SelectedEasing);

                offset += periodOffset;
            }
            return doubleToAnimate;
        }

        private bool _invert;
        public bool Invert
        {
            get => _invert;
            set => SetAndNotify(ref _invert, value);
        }
    }
}