using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Composition : Component, IBeat
    {
        public Composition(int id, MessengerTerminal messengerTerminal) : base (id, messengerTerminal)
        {
            MasterBeat = new MasterBeat(nameof(MasterBeat), this);
            Transition = new Slider(nameof(Transition), this);
            Camera = new Camera(nameof(Camera), this, MasterBeat);
            ComponentFactory = new LayerFactory(messengerTerminal);
        }

        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        public Beat Beat { get; set; }
        public MasterBeat MasterBeat { get; set; }
    }
}