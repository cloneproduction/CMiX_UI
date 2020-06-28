using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Composition : Component
    {
        public Composition(int id, Beat beat) : base (id, beat)
        {
            //Transition = new Slider(nameof(Transition));
            //Camera = new Camera(Beat);
        }

        //public Camera Camera { get; set; }
        //public Slider Transition { get; set; }
    }
}