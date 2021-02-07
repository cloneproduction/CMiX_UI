using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Coloration : Sender, IBeatModifier
    {
        public Coloration(string name, IColleague parentSender, MasterBeat masterBeat) : base (name, parentSender)
        {
            BeatModifier = new BeatModifier(nameof(BeatModifier), this, masterBeat);
            ColorSelector = new ColorSelector(nameof(ColorSelector), this);
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as ColorationModel);
        }

        public ColorSelector ColorSelector { get; set; }
        public BeatModifier BeatModifier { get; set; }

        public override void Dispose()
        {
            BeatModifier.Dispose();
            ColorSelector.Dispose();
            base.Dispose();
        }
    }
}