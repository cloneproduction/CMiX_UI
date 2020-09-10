using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class None : AnimMode
    {
        public None()
        {

        }

        public None(Sendable parentSendable) : this()
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
                animParameter.Parameters[i] = animParameter.DefaultValue;
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