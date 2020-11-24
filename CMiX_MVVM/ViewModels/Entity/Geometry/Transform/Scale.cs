using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Scale : ViewModel, IColleague
    {
        public Scale(string name, IColleague parentSender)
        {
            this.Address = $"{parentSender.Address}{name}/";
            this.MessageMediator = parentSender.MessageMediator;
            this.MessageMediator.RegisterColleague(this);

            Uniform = new Slider(nameof(Uniform), this);
            Uniform.Amount = 1.0;

            X = new Slider(nameof(X), this);
            X.Amount = 1.0;

            Y = new Slider(nameof(Y), this);
            Y.Amount = 1.0;

            Z = new Slider(nameof(Z), this);
            Z.Amount = 1.0;

            IsUniform = true;
        }

        public MessageMediator MessageMediator { get; set; }
        public string Address { get; set; }
        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }
        public Slider Uniform { get; set; }

        private bool _isUniform;
        public bool IsUniform
        {
            get => _isUniform;
            set => SetAndNotify(ref _isUniform, value);
        }

        public void Send(Message message)
        {
            MessageMediator?.Notify(MessageDirection.OUT, message);
        }

        public void Receive(Message message)
        {
            this.SetViewModel(message.Obj as ScaleModel);
        }
    }
}
