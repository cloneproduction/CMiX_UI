using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Scene : Component, IBeatModifier
    {
        public Scene(int id, MessageTerminal MessageTerminal, MasterBeat masterBeat) : base (id, MessageTerminal)
        {
            MasterBeat = masterBeat;
            BeatModifier = new BeatModifier(nameof(BeatModifier), this, masterBeat);
            PostFX = new PostFX(nameof(PostFX), this);
            Mask = new Mask(nameof(Mask), this);
            Transform = new Transform(nameof(Transform), this);
            ComponentFactory = new EntityFactory(MessageTerminal);
        }

        
        public Transform Transform { get; set; }
        public Mask Mask { get; set; }
        public PostFX PostFX { get; set; }
        public BeatModifier BeatModifier { get; set; }
        public MasterBeat MasterBeat { get; set; }
    }
}