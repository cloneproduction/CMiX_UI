using CMiX.MVVM.Resources;

namespace CMiX.MVVM.ViewModels
{
    public class LFO : AnimMode
    {
        public LFO(AnimParameter animParameter)
        {
            AnimParameter = animParameter;
        }

        public LFO(AnimParameter animParameter, Sendable parentSendable) : this(animParameter)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void UpdateOnBeatTick(double period)
        {

        }

        public override double UpdatePeriod(double period)
        {
            return Utils.Map(Easings.Interpolate((float)period, AnimParameter.Easing.SelectedEasing), 0.0, 1.0, AnimParameter.Range.Minimum, AnimParameter.Range.Maximum);
        }

        private bool _invert;
        public bool Invert
        {
            get => _invert;
            set => SetAndNotify(ref _invert, value);
        }
    }
}