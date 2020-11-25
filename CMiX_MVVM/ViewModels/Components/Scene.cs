using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Scene : Component, ITransform, IBeatModifier
    {
        public Scene(int id, MessengerTerminal messengerTerminal, MasterBeat masterBeat) : base(id, messengerTerminal)
        {
            MasterBeat = masterBeat;
            BeatModifier = new BeatModifier(nameof(BeatModifier), this, masterBeat);
            PostFX = new PostFX(nameof(PostFX), this);
            Mask = new Mask(nameof(Mask), this);
            Transform = new Transform(nameof(Transform), this);
        }

        public Transform Transform { get; set; }
        public Mask Mask { get; set; }
        public PostFX PostFX { get; set; }
        public BeatModifier BeatModifier { get; set; }
        public MasterBeat MasterBeat { get; set; }
    }
}