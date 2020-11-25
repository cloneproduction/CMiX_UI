using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Range : Sender
    {
        public Range(string name, IColleague parentSender, double minimum = 0.0, double maximum = 1.0) : base (name, parentSender)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as RangeModel);
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
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
            }
        }

        private double _maximum;
        public double Maximum
        {
            get => _maximum;
            set
            {
                SetAndNotify(ref _maximum, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
            }
        }
    }
}
