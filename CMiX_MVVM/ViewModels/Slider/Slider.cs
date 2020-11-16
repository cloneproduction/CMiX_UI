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
        }

        public Slider(string name, IColleague parentSender, MessageMediator messageMediator) : this(name, parentSender)
        {
            this.MessageMediator = messageMediator;
            this.MessageMediator.RegisterColleague(this);
        }


        public string Address { get; set; }

        public MessageMediator MessageMediator { get; set; }

        public void Send(Message message)
        {
            if(MessageMediator != null)
                MessageMediator.Notify(this.Address, this, message);
        }

        public void Receive(Message message)
        {
            var model = MessageSerializer.Serializer.Deserialize<SliderModel>(message.Data);
            this.SetViewModel(model);
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
                Send(new Message(MessageDirection.OUT, this.Address, MessageSerializer.Serializer.Serialize(this.GetModel())));
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
