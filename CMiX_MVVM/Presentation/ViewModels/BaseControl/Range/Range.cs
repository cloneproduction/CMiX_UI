using CMiX.Core.Interfaces;
using CMiX.Core.MessageService;
using CMiX.Core.Models;
using System;

namespace CMiX.Core.Presentation.ViewModels
{
    public class Range : ViewModel, IControl, IRange
    {
        public Range(RangeModel rangeModel) 
        {
            this.ID = rangeModel.ID;
            Minimum = rangeModel.Minimum;
            Maximum = rangeModel.Maximum;
        }


        public Guid ID { get; set; }
        public ControlCommunicator Communicator { get; set; }

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


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);
        }


        public void SetViewModel(IModel model)
        {
            RangeModel rangeModel = model as RangeModel;
            this.ID = rangeModel.ID;
            this.Minimum = rangeModel.Minimum;
            this.Maximum = rangeModel.Maximum;
        }

        public IModel GetModel()
        {
            IRangeModel model = new RangeModel();
            model.ID = this.ID;
            model.Minimum = this.Minimum;
            model.Maximum = this.Maximum;
            return model;
        }
    }
}
