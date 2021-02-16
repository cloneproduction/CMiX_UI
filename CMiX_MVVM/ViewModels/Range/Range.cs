using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Range : Sender, IRange
    {
        public Range(string name, IMessageProcessor parentSender, double minimum = 0.0, double maximum = 1.0) : base (name, parentSender)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        private double _width;
        public double Width
        {
            get => _width;
            set => SetAndNotify(ref _width, value);
        }

        private double _minimum;
        public double Minimum
        {
            get => _minimum;
            set
            {
                SetAndNotify(ref _minimum, value);
                Width = Math.Abs(Maximum - Minimum);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
            }
        }

        private double _maximum;
        public double Maximum
        {
            get => _maximum;
            set
            {
                SetAndNotify(ref _maximum, value);
                Width = Math.Abs(Maximum - Minimum);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
            }
        }

        public override void SetViewModel(IModel model)
        {
            RangeModel rangeModel = model as RangeModel;
            this.Minimum = rangeModel.Minimum;
            this.Maximum = rangeModel.Maximum;
        }

        public override IModel GetModel()
        {
            IRangeModel model = new RangeModel();
            model.Minimum = this.Minimum;
            model.Maximum = this.Maximum;
            return model;
        }
    }
}
