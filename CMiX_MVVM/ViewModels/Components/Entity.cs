using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Entity : Component, IBeat
    {
        public Entity(int id, MessengerTerminal messengerTerminal, MasterBeat beat) : base(id, messengerTerminal)
        {
            BeatModifier = new BeatModifier(beat);
            Geometry = new Geometry(beat);
            Texture = new Texture();
            Coloration = new Coloration(beat);
        }

        public BeatModifier BeatModifier { get; }
        public Geometry Geometry { get; }
        public Texture Texture { get; }
        public Coloration Coloration { get; }
        public MasterBeat MasterBeat { get; set; }
    }
}