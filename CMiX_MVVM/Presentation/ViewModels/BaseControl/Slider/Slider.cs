using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;
using System;
using System.Windows.Input;

namespace CMiX.Core.Presentation.ViewModels
{
    public class Slider : ViewModel, IControl
    {
        public Slider(string name, SliderModel sliderModel)
        {
            Name = name;

            this.ID = sliderModel.ID;
            this.Amount = sliderModel.Amount;
            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
            ResetCommand = new RelayCommand(p => Reset());
        }


        public Guid ID { get; set; }
        public ControlCommunicator Communicator { get; set; }

        public ICommand AddCommand { get; }
        public ICommand SubCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand MouseDownCommand { get; }


        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private double _amount;
        public double Amount
        {
            get => _amount;
            set
            {
                SetAndNotify(ref _amount, value);
                Communicator?.SendMessageUpdateViewModel(this);
            }
        }

        private double _minimum = 0.0;
        public double Minimum
        {
            get => _minimum;
            set => SetAndNotify(ref _minimum, value);
        }

        private double _maximum = 1.0;
        public double Maximum
        {
            get => _maximum;
            set => SetAndNotify(ref _maximum, value);
        }


        private void Add() => Amount = Amount >= Maximum ? Maximum : Amount += 0.01;
        private void Sub() => Amount = Amount <= Minimum ? Minimum : Amount -= 0.01;
        public void Reset() => Amount = 0.0;


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
            SliderModel sliderModel = model as SliderModel;
            this.ID = sliderModel.ID;
            this.Amount = sliderModel.Amount;
            System.Console.WriteLine("Slider SetViewModel Amount " + Amount);
        }

        public IModel GetModel()
        {
            SliderModel model = new SliderModel();
            model.ID = this.ID;
            model.Amount = this.Amount;
            return model;
        }
    }
}