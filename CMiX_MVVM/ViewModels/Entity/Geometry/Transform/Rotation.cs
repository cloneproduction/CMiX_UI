using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Rotation : Sender
    {
        public Rotation()
        {
            X = new Slider(nameof(X));
            Y = new Slider(nameof(Y));
            Z = new Slider(nameof(Z));
        }

        public Rotation(Sender parentSender) : this()
        {
            SubscribeToEvent(parentSender);
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