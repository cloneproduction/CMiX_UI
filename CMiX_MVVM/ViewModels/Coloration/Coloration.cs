using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Coloration : Sender
    {
        public Coloration(string name, IColleague parentSender, MasterBeat beat) :base(name, parentSender)
        {
            BeatModifier = new BeatModifier(nameof(BeatModifier), this, beat);
            ColorSelector = new ColorSelector(nameof(ColorSelector), this);
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as ColorationModel);
        }

        public ColorSelector ColorSelector { get; set; }
        public BeatModifier BeatModifier { get; set; }
    }
}