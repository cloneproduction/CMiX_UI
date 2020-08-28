using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Steady : AnimMode, IAnimMode
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

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as SteadyModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public void UpdateOnBeatTick(double period)
        {
            
        }

        public double UpdatePeriod(double period, AnimParameter animParameter)
        {
            return period;
        }

        private SteadyType _steadyType;
        public SteadyType SteadyType
        {
            get => _steadyType;
            set => SetAndNotify(ref _steadyType, value);
        }
        public AnimParameter AnimParameter { get; set; }
    }
}
