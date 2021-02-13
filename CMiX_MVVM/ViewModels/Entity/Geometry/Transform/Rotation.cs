using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels
{
    public class Rotation : Sender
    {
        public Rotation(string name, IMessageProcessor parentSender) : base (name, parentSender)
        {
            X = new Slider(nameof(X), this);
            Y = new Slider(nameof(Y), this);
            Z = new Slider(nameof(Z), this);
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }

        public override void Receive(IMessage message)
        {
            this.SetViewModel(message.Obj as RotationModel);
        }
    }
}