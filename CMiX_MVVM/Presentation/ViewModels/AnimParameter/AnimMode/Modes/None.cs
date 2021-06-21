using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Presentation.ViewModels.Beat;
using System;

namespace CMiX.Core.Presentation.ViewModels
{
    public class None : ViewModel, IControl, IAnimMode
    {
        public None(AnimParameter animParameter, NoneModel noneModel)
        {

        }


        public ControlCommunicator Communicator { get; set; }
        public Guid ID { get; set; }
        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetAndNotify(ref _IsEnabled, value);
        }


        public void UpdateOnBeatTick(double[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier)
        {

        }

        public void UpdateOnGameLoop(double[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier)
        {

        }


        public void SetCommunicator(Communicator communicator)
        {
            throw new NotImplementedException();
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            throw new NotImplementedException();
        }


        public void SetViewModel(IModel model)
        {
            throw new System.NotImplementedException();
        }

        public IModel GetModel()
        {
            throw new System.NotImplementedException();
        }
    }
}