using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class None : AnimMode, IAnimMode
    {
        public None()
        {

        }

        public None(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void UpdateOnBeatTick(double period)
        {
            //throw new System.NotImplementedException();
        }

        public override double UpdatePeriod(double period)
        {
            return period;
        }

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetAndNotify(ref _IsEnabled, value);
        }
        public AnimParameter AnimParameter { get; set; }
    }
}