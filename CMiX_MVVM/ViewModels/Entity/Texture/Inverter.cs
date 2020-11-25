using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Inverter : Sender
    {
        public Inverter(string name, IColleague parentSender) : base (name, parentSender)
        {
            Invert = new Slider(nameof(Invert), this);
            InvertMode = ((TextureInvertMode)0).ToString();
        }

        public Slider Invert { get; set; }

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

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as InverterModel);
        }
    }
}