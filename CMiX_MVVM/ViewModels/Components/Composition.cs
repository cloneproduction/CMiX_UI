using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Composition : Component, IBeat
    {
        public Composition(int id, MasterBeat beat) : base (id)
        {
            Transition = new Slider(nameof(Transition));
            Beat = beat;
            beat.SubscribeToEvent(this);
            Camera = new Camera(beat);
        }

        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        public MasterBeat Beat { get; set; }
    }
}