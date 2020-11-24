using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Transform : ViewModel, IColleague
    {
        public Transform(string name, IColleague parentSender)
        {
            this.Address = $"{parentSender.Address}{name}/";
            this.MessageMediator = parentSender.MessageMediator;
            this.MessageMediator.RegisterColleague(this);

            Translate = new Translate(nameof(Translate), this);
            Scale = new Scale(nameof(Scale), this);
            Rotation = new Rotation(nameof(Rotation), this);

            Is3D = false;
        }

        public MessageMediator MessageMediator { get; set; }
        public string Address { get; set; }

        public void Send(Message message)
        {
            MessageMediator?.Notify(MessageDirection.OUT, message);
        }

        public void Receive(Message message)
        {
            this.SetViewModel(message.Obj as TransformModel);
        }

        public Translate Translate { get; set; }
        public Scale Scale { get; set; }
        public Rotation Rotation { get; set; }

        private bool _is3D;
        public bool Is3D
        {
            get => _is3D;
            set
            {
                SetAndNotify(ref _is3D, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
            }
        }
    }
}