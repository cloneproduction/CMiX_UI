using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Services;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class Composition : Component
    {
        #region CONSTRUCTORS
        public Composition(int id,  Beat beat, Mementor mementor) 
            : base (id, beat, mementor)
        {
            //MessageValidationManager = new MessageValidationManager(MessengerService);
            Transition = new Slider("/Transition", Mementor);
            Camera = new Camera(MessageAddress, Beat, Mementor);
        }
        #endregion
        public MessengerService MessengerService { get; set; }
        #region PROPERTIES
        public MessageValidationManager MessageValidationManager { get; set; }
        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        #endregion
    }
}