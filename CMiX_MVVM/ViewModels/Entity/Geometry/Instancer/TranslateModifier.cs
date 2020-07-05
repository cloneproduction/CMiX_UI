using System.Windows;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class TranslateModifier : Sendable
    {
        public TranslateModifier(Beat beat) 
        {
            Translate = new AnimParameter(nameof(Translate), beat, true, this);
            TranslateX = new AnimParameter(nameof(TranslateX), beat, false, this);
            TranslateY = new AnimParameter(nameof(TranslateY), beat, false, this);
            TranslateZ = new AnimParameter(nameof(TranslateZ), beat, false, this);
        }

        public TranslateModifier(Beat beat, Sendable parentSendable) : this(beat)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as TranslateModifierModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public AnimParameter Translate { get; set; }
        public AnimParameter TranslateX { get; set; }
        public AnimParameter TranslateY { get; set; }
        public AnimParameter TranslateZ { get; set; }
    }
}
