using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels
{
    public class BlendMode : Sender
    {
        public BlendMode(string name, IMessageProcessor parentSender)  : base(name, parentSender)
        {
            Mode = ((BlendModeEnum)0).ToString();
        }

        private string _mode;
        public string Mode
        {
            get { return _mode; }
            set
            {
                //Mementor.PropertyChange(this, nameof(Mode));
                SetAndNotify(ref _mode, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.GetAddress(), this.GetModel()));
            }
        }

        public override void Receive(IMessage message)
        {
            this.SetViewModel(message.Obj as BlendModeModel);
            System.Console.WriteLine("POUETPOUET " + this.GetAddress() + "BlendMode received " + message.Address + "  " + this.Mode);
        }
    }
}
