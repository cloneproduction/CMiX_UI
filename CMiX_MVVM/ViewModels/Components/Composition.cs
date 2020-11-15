using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;

namespace CMiX.MVVM.ViewModels
{
    public class Composition : Component, IBeat
    {
        public Composition(int id, MasterBeat masterBeat) : base (id)
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