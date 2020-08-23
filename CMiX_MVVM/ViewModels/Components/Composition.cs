using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.ViewModels
{
    public class Composition : Component, IBeat
    {
        public Composition(int id) : base (id)
        {
            Transition = new Slider(nameof(Transition));
            MasterBeat = new MasterBeat(this);
            Camera = new Camera(MasterBeat);
        }

        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        public MasterBeat MasterBeat { get; set; }
        public Beat Beat { get; set; }
    }
}