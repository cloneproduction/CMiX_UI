using CMiX.MVVM.Interfaces;
using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Composition : Component, IBeat
    {
        public Composition(int id, MessengerManager messengerManager, MasterBeat masterBeat) : base (id, messengerManager)
        {
            Transition = new Slider(nameof(Transition));
            Camera = new Camera(masterBeat);
            MasterBeat = masterBeat;
        }

        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        public Beat Beat { get; set; }
        public MasterBeat MasterBeat { get; set; }
    }
}