using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Services;
using Memento;
using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public class Composition : Component
    {
        #region CONSTRUCTORS
        public Composition(int id, Beat beat) 
            : base (id, beat)
        {
            Messengers = new ObservableCollection<Messenger>();
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