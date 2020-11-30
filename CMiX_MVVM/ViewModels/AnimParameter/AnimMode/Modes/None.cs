using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class None : Sender, IAnimMode
    {
        public None(string name, AnimParameter parentSender) : base (name, parentSender)
        {

        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as NoneModel);
        }

        public void UpdateOnBeatTick(double[] doubleToAnimate, double period, Range range, Easing easing)
        {

        }

        public void UpdateOnGameLoop(double[] doubleToAnimate, double period, Range range, Easing easing)
        {

        }

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetAndNotify(ref _IsEnabled, value);
        }
    }
}