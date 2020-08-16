using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Coloration : Sendable
    {
        public Coloration(MasterBeat beat) 
        {
            BeatModifier = new BeatModifier(beat);
            ColorSelector = new ColorSelector(this);
        }

        public Coloration(MasterBeat beat, Sendable parentSendable) : this(beat)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as ColorationModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public ColorSelector ColorSelector { get; }
        public BeatModifier BeatModifier { get; }
    }
}