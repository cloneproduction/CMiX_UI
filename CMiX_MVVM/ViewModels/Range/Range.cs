using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Range : Module, IRange
    {
        public Range(RangeModel rangeModel) 
        {
            Minimum = rangeModel.Minimum;
            Maximum = rangeModel.Maximum;
        }

        public override void SetModuleReceiver(ModuleMessageReceiver messageDispatcher)
        {
            messageDispatcher.RegisterMessageReceiver(this);
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
