using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public class Composition : Component
    {
        #region CONSTRUCTORS
        public Composition(int id, Beat beat) 
            : base (id, beat)
        {
            Transition = new Slider("/Transition");
            Camera = new Camera(MessageAddress, Beat);
        }


        #endregion

        #region PROPERTIES
        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        #endregion
    }
}