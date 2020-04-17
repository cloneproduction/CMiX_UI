using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Services;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class Composition : Component
    {
        #region CONSTRUCTORS
        public Composition(int id, string messageAddress, Beat beat, MessageService messageService, Mementor mementor) 
            : base (id, beat, messageAddress, messageService, mementor)
        {
            MessageValidationManager = new MessageValidationManager(MessageService);
            Transition = new Slider("/Transition", MessageService, Mementor);
            Camera = new Camera(MessageService, MessageAddress, Beat, Mementor);
        }
        #endregion

        #region PROPERTIES
        public MessageValidationManager MessageValidationManager { get; set; }
        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
        #endregion
    }
}