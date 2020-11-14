using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;

namespace CMiX.MVVM.ViewModels
{
    public class Composition : Component, IBeat
    {
        public Composition(int id) : base (id)
        {
            Transition = new Slider(nameof(Transition));
            MasterBeat = new MasterBeat(this);
            Camera = new Camera(MasterBeat);
            Factory = new LayerFactory();
        }

        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        public Beat Beat { get; set; }
    }
}