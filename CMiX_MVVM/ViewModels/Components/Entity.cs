using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Entity : Component, IBeat
    {
        public Entity(int id, MessengerManager messengerManager, MasterBeat beat) : base(id, messengerManager)
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