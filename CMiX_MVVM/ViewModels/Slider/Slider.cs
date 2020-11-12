using System.Windows.Input;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.Services.Message;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Slider : Sender, IColleague
    {
        public Slider(string name)
        {
            Name = name;

            AddCommand = new RelayCommand(p => Add());
            SubCommand = new RelayCommand(p => Sub());
            ResetCommand = new RelayCommand(p => Reset());
        }

        public Slider(string name, Sender parentSender) : this(name)
        {
            this.Address = $"{parentSender.Address}/{Name}/"; //parentSender.Address + this.Name;
            SubscribeToEvent(parentSender);
        }

        public Slider(string name, Sender parentSender, MessageMediator messageMediator) : this(name, parentSender)
        {
            this.MessageMediator = messageMediator;
            this.MessageMediator.RegisterColleague(this.Address, this);
        }



        ///MEDIATOR TEST
        ///
        public MessageMediator MessageMediator { get; set; }

        public void Send(MessageReceived message)
        {
            if(MessageMediator != null)
                MessageMediator.Notify(this.Address, this, message);
        }

        public void Receive(MessageReceived message)
        {
            var model = MessageSerializer.Serializer.Deserialize<SliderModel>(message.Data);
            this.SetViewModel(model);
            System.Console.WriteLine("POUETPOUET " + this.Address + " received " + message.Address);
        }

        ////////////////////////









        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            //System.Console.WriteLine("Slider Receive Change");
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as SliderModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }


        public ICommand AddCommand { get; }
        public ICommand SubCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand MouseDownCommand { get; }


        public override string GetMessageAddress()
        {
            return $"{Name}/";
        }

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
                OnSendChange(this.GetModel(), this.GetMessageAddress());
                Send(new MessageReceived(MessageDirection.OUT, this.GetMessageAddress(), MessageSerializer.Serializer.Serialize(this.GetModel())));
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
