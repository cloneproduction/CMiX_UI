using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;

namespace CMiX.MVVM.ViewModels
{
    public class Scene : Component, ITransform, IBeatModifier
    {
        public Scene(int id, MasterBeat beat) : base(id)
        {
            MasterBeat = beat;
            BeatModifier = new BeatModifier(beat);
            PostFX = new PostFX();
            Mask = new Mask();
            Transform = new Transform();
            Factory = new EntityFactory();
        }

        public Transform Transform { get; set; }
        public Mask Mask { get; set; }
        public PostFX PostFX { get; set; }
        public BeatModifier BeatModifier { get; set; }
    }
}