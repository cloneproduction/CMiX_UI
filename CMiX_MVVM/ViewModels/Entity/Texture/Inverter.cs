using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Inverter : Sender
    {
        public Inverter()
        {
            Invert = new Slider(nameof(Invert));
            InvertMode = ((TextureInvertMode)0).ToString();
        }

        public Inverter(Sender parentSender) : this()
        {
            SubscribeToEvent(parentSender);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as InverterModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public Slider Invert { get; }

        private string _invertMode;
        public string InvertMode
        {
            get => _invertMode;
            set
            {
                SetAndNotify(ref _invertMode, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }
    }
}
