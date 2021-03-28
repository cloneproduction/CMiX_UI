using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public class Slider : MessageCommunicator
    {
        public Slider(string name, MessageDispatcher messageDispatcher, SliderModel sliderModel) 
            : base (messageDispatcher, sliderModel)
        {
            this.ID = sliderModel.ID;
            this.Amount = sliderModel.Amount;
            Name = name;

            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
            ResetCommand = new RelayCommand(p => Reset());
        }


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
                RaiseMessageNotification();
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


        public override void SetViewModel(IModel model)
        {
            SliderModel sliderModel = model as SliderModel;
            this.Amount = sliderModel.Amount;
            System.Console.WriteLine("Slider SetViewModel Amount " + Amount);
        }

        public override IModel GetModel()
        {
            SliderModel model = new SliderModel();
            model.Amount = this.Amount;
            return model;
        }
    }
}
