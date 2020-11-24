using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Rotation : ViewModel, IColleague
    {
        public Rotation(string name, IColleague parentSender)
        {
            this.Address = $"{parentSender.Address}{name}/";
            this.MessageMediator = parentSender.MessageMediator;
            this.MessageMediator.RegisterColleague(this);

            X = new Slider(nameof(X), this);
            Y = new Slider(nameof(Y), this);
            Z = new Slider(nameof(Z), this);
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }
        public MessageMediator MessageMediator { get; set; }
        public string Address { get; set; }

        public void Send(Message message)
        {
            MessageMediator?.Notify(MessageDirection.OUT, message);
        }

        public void Receive(Message message)
        {
            this.SetViewModel(message.Obj as RotationModel);
        }
    }
}