using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.ViewModels
{
    public class Entity : Component, IBeat
    {
        public Entity(int id, MasterBeat beat) : base(id)
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