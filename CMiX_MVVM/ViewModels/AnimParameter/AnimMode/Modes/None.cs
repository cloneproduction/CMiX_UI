using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class None : Sender, IAnimMode
    {
        public None(string name, IColleague parentSender) : base (name, parentSender)
        {

        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as NoneModel);
        }

        public void UpdateOnBeatTick(AnimParameter animParameter, double period)
        {

        }

        public void UpdateParameters(AnimParameter animParameter, double period)
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