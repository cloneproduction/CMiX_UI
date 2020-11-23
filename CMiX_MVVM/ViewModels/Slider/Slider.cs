using System.Windows.Input;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Slider : ViewModel, IColleague
    {
        public Slider(string name)
        {
            Name = name;

            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
            ResetCommand = new RelayCommand(p => Reset());
        }

        public Slider(string name, IColleague parentSender) : this(name)
        {
            this.Address = $"{parentSender.Address}{Name}/";
            System.Console.WriteLine("Slider Constructor Call");
        }

        public Slider(string name, IColleague parentSender, MessageMediator messageMediator) : this(name, parentSender)
        {
            this.MessageMediator = messageMediator;
            this.MessageMediator.RegisterColleague(this);
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set 
            {
                _address = value;
                System.Console.WriteLine("Slider " + this.Name + " AddressSetto " + value);
            }
        }

        //public string Address { get; set; }

        public MessageMediator MessageMediator { get; set; }

        public void Send(Message message)
        {
            MessageMediator?.Notify(MessageDirection.OUT, message);
        }

        public void Receive(Message message)
        {
            this.SetViewModel(message.Obj as SliderModel);
            System.Console.WriteLine("POUETPOUET " + this.Address + "Slider received " + message.Address +"  "  + this.Amount);
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
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
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
        

        private void Add()
        {
            if (Amount >= Maximum)
                Amount = Maximum;
            else
                Amount += 0.01;
        }

        private void Sub()
        {
            if (Amount <= Minimum)
                Amount = Minimum;
            else
                Amount -= 0.01;
        }

        public void Reset()
        {
            Amount = 0.0;
        }
    }
}
