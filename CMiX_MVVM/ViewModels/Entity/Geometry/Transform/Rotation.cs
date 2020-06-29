using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Rotation : Sendable
    {
        public Rotation()
        {
            X = new Slider(nameof(X), this);
            Y = new Slider(nameof(Y), this);
            Z = new Slider(nameof(Z), this);
        }

        public Rotation(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as RotationModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }
    }
}