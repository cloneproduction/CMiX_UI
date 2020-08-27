using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class LFO : Sendable, IAnimMode
    {
        public LFO()
        {
            //Update = UpdatePosition;
        }

        public LFO(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {

        }

        private double map(double value, double fromLow, double fromHigh, double toLow, double toHigh)
        {
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }

        public Func<double, AnimParameter, double> Update { get; set; }

        public double UpdatePosition(double period, AnimParameter animParameter)
        {
            //return map(Easings.Interpolate((float)period, animParameter.Easing.SelectedEasing), 0.0, 1.0, animParameter.Range.Minimum, animParameter.Range.Maximum);
            return period;
        }

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetAndNotify(ref _IsEnabled, value);
        }

        private bool _invert;
        public bool Invert
        {
            get => _invert;
            set => SetAndNotify(ref _invert, value);
        }
    }
}