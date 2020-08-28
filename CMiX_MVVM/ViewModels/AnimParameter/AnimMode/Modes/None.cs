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

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as NoneModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public void UpdateOnBeatTick(double period)
        {
            //throw new System.NotImplementedException();
        }

        public double UpdatePeriod(double period, AnimParameter animParameter)
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