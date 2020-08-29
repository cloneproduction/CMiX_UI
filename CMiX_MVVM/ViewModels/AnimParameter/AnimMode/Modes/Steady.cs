using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Steady : AnimMode
    {
        public Steady(AnimParameter animParameter, double defaultValue)
        {
            SteadyType = SteadyType.Linear;
            DefaultValue = defaultValue;
            AnimParameter = animParameter;
        }
 
        public Steady(AnimParameter animParameter, double defaultValue, Sendable parentSendable) : this(animParameter, defaultValue)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void UpdateOnBeatTick(double period)
        {
            
        }

        public override double UpdatePeriod(double period)
        {
            return DefaultValue;
        }

        private SteadyType _steadyType;
        public SteadyType SteadyType
        {
            get => _steadyType;
            set => SetAndNotify(ref _steadyType, value);
        }
    }
}
