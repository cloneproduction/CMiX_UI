using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Inverter : ViewModel, IColleague
    {
        public Inverter(string name, IColleague parentSender)
        {
            this.Address = $"{parentSender.Address}{name}/";
            this.MessageMediator = parentSender.MessageMediator;
            this.MessageMediator.RegisterColleague(this);

            Invert = new Slider(nameof(Invert), this);
            InvertMode = ((TextureInvertMode)0).ToString();
        }

        public string Address { get; set; }
        public Slider Invert { get; }
        public MessageMediator MessageMediator { get; set; }

        private string _invertMode;
        public string InvertMode
        {
            get => _invertMode;
            set
            {
                SetAndNotify(ref _invertMode, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
            }
        }

        public void Send(Message message)
        {
            MessageMediator?.Notify(MessageDirection.OUT, message);
        }

        public void Receive(Message message)
        {
            this.SetViewModel(message.Obj as InverterModel);
            System.Console.WriteLine("POUETPOUET " + this.Address + "Inverter received " + this.InvertMode);
        }
    }
}