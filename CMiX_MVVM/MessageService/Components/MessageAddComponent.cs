using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public class MessageAddComponent : Message, IComponentMessage
    {
        public MessageAddComponent()
        {

        }

        public MessageAddComponent(Component component)
        {
            ComponentModel = component.GetModel() as IComponentModel;
        }

        public IComponentModel ComponentModel { get; set; } // must be public because of Ceras...
    }
}