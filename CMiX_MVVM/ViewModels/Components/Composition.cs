using System.Collections.ObjectModel;

namespace CMiX.MVVM.ViewModels
{
    public class Composition : Component
    {
        #region CONSTRUCTORS
        public Composition(int id, Beat beat) 
            : base (id, beat)
        {
            Transition = new Slider("/Transition");
            Camera = new Camera(Beat);
        }


        #endregion

        #region PROPERTIES
        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        #endregion
    }
}