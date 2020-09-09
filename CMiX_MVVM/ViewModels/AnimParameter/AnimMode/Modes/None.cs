using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class None : AnimMode
    {
        public None(AnimParameter animParameter)
        {
            AnimParameter = animParameter;
        }

        public None(AnimParameter animParameter, Sendable parentSendable) : this(animParameter)
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

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetAndNotify(ref _IsEnabled, value);
        }
    }
}