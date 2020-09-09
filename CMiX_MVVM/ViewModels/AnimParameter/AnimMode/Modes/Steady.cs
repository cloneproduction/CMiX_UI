using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Steady : AnimMode
    {
        public Steady(AnimParameter animParameter)
        {
            SteadyType = SteadyType.Linear;
            AnimParameter = animParameter;
        }
 
        public Steady(AnimParameter animParameter, Sendable parentSendable) : this(animParameter)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void UpdateOnBeatTick(double period)
        {
            
        }

        public override double UpdatePeriod(double period)
        {
            return AnimParameter.DefaultValue;
        }

        private SteadyType _steadyType;
        public SteadyType SteadyType
        {
            get => _steadyType;
            set => SetAndNotify(ref _steadyType, value);
        }
    }
}
