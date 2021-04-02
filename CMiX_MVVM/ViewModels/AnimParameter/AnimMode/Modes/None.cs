using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class None : MessageCommunicator, IAnimMode
    {
        public None(AnimParameter animParameter, MessageDispatcher messageDispatcher, NoneModel noneModel)
        {

        }

        public void UpdateOnBeatTick(double[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier)
        {

        }

        public void UpdateOnGameLoop(double[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier)
        {

        }

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetAndNotify(ref _IsEnabled, value);
        }

        public override void SetViewModel(IModel model)
        {
            throw new System.NotImplementedException();
        }

        public override IModel GetModel()
        {
            throw new System.NotImplementedException();
        }
    }
}