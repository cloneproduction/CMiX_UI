using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.ViewModels
{
    public class Scene : Component, ITransform, IBeatModifier
    {
        public Scene(int id, Beat beat) : base(id, beat)
        {
            BeatModifier = new BeatModifier(beat);
            PostFX = new PostFX();
            Mask = new Mask(this);
            Transform = new Transform(this);
        }

        public Transform Transform { get; set; }
        public Mask Mask { get; set; }
        public BeatModifier BeatModifier { get; set; }
        public PostFX PostFX { get; set; }
    }
}