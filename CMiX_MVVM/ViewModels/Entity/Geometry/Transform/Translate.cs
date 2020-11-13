using System.Windows;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Translate : Sender
    {
        public Translate()
        {
            X = new Slider(nameof(X));
            Y = new Slider(nameof(Y));
            Z = new Slider(nameof(Z));
        }

        public Translate(Sender parentSender) : this()
        {
            SubscribeToEvent(parentSender);
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as TranslateModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }
    }
}
