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
            double min = animParameter.Range.Minimum;
            double max = animParameter.Range.Maximum;

            double periodOffset = 1.0 / animParameter.Parameters.Length;
            double offset = 0.0;
            double val = 0.0;

            for (int i = 0; i < animParameter.Parameters.Length; i++)
            {
                val = period + offset;
                if (val < 0.0)
                    val = 1.0 - (0.0 - val) % (1.0 - 0.0);
                else
                    val = 0.0 + (val - 0.0) % (1.0 - 0.0);

                animParameter.Parameters[i] = Easings.Interpolate((float)Utils.Map(val, 0.0, 1.0, min, max), animParameter.Easing.SelectedEasing);

                offset += periodOffset;
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