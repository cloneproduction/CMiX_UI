using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class LFO : AnimMode, IAnimMode
    {
        public LFO(AnimParameter animParameter)
        {
            AnimParameter = animParameter;
        }

        public LFO(AnimParameter animParameter, Sendable parentSendable) : this(animParameter)
        {
            SubscribeToEvent(parentSendable);
        }

        public AnimParameter AnimParameter { get; set; }

        public override void UpdateOnBeatTick(double period)
        {

        }

        public override double UpdatePeriod(double period, AnimParameter animParameter)
        {
            return Utils.Map(Easings.Interpolate((float)period, animParameter.Easing.SelectedEasing), 0.0, 1.0, animParameter.Range.Minimum, animParameter.Range.Maximum);
        }

        private bool _invert;
        public bool Invert
        {
            get => _invert;
            set => SetAndNotify(ref _invert, value);
        }
    }
}