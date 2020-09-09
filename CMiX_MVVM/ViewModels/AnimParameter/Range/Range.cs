using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Range : Sendable
    {
        public Range(double minimum = 0.0, double maximum = 1.0)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public Range(double minimum, double maximum, Sendable parentSendable) : this(minimum = 0.0, maximum = 1.0)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as RangeModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public double Distance
        {
            get => Math.Abs(Maximum - Minimum);
        }

        private double _minimum;
        public double Minimum
        {
            get => _minimum;
            set
            {
                SetAndNotify(ref _minimum, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        private double _maximum;
        public double Maximum
        {
            get => _maximum;
            set
            {
                SetAndNotify(ref _maximum, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }
    }
}
