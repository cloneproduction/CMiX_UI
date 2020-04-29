using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Services;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class Composition : Component
    {
        #region CONSTRUCTORS
        public Composition(int id,  Beat beat, MessengerService messengerService, Mementor mementor) 
            : base (id, beat, messengerService, mementor)
        {
            //MessageValidationManager = new MessageValidationManager(MessengerService);
            Transition = new Slider("/Transition", MessengerService, Mementor);
            Camera = new Camera(MessengerService, MessageAddress, Beat, Mementor);
        }
        #endregion

        #region PROPERTIES
        public MessageValidationManager MessageValidationManager { get; set; }
        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        #endregion
    }
}