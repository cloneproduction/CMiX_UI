using CMiX.MVVM.Interfaces;
using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Scene : Component, ITransform, IBeatModifier
    {
        public Scene(int id, MessengerManager messengerManager, MasterBeat masterBeat) : base(id, messengerManager)
        {
            MasterBeat = masterBeat;
            BeatModifier = new BeatModifier(masterBeat);
            PostFX = new PostFX();
            Mask = new Mask();
            Transform = new Transform();

        }

        public Transform Transform { get; set; }
        public Mask Mask { get; set; }
        public PostFX PostFX { get; set; }
        public BeatModifier BeatModifier { get; set; }
        public MasterBeat MasterBeat { get; set; }
    }
}