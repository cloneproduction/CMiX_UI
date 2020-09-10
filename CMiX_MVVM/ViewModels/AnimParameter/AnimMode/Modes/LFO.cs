using CMiX.MVVM.Resources;

namespace CMiX.MVVM.ViewModels
{
    public class LFO : AnimMode
    {
        public LFO()
        {

        }

        public LFO(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void UpdateOnBeatTick(AnimParameter animParameter, double period)
        {

        }

        public override void UpdateParameters(AnimParameter animParameter, double period)
        {
            for (int i = 0; i < animParameter.Parameters.Length; i++)
            {
                animParameter.Parameters[i] = Utils.Map(Easings.Interpolate((float)period, animParameter.Easing.SelectedEasing), 0.0, 1.0, animParameter.Range.Minimum, animParameter.Range.Maximum); ;
            }
            
        }

        private bool _invert;
        public bool Invert
        {
            get => _invert;
            set => SetAndNotify(ref _invert, value);
        }
    }
}