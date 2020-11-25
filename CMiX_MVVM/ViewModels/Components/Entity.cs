using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Entity : Component, IBeat
    {
        public Entity(int id, MessengerTerminal messengerTerminal, MasterBeat beat) : base(id, messengerTerminal)
        {
            BeatModifier = new BeatModifier(nameof(BeatModifier), this, beat);
            Geometry = new Geometry(nameof(Geometry), this, beat);
            Texture = new Texture(nameof(Texture), this);
            Coloration = new Coloration(nameof(Coloration), this, beat);
        }

        public BeatModifier BeatModifier { get; set; }
        public Geometry Geometry { get; set; }
        public Texture Texture { get; set; }
        public Coloration Coloration { get; set; }
        public MasterBeat MasterBeat { get; set; }
    }
}