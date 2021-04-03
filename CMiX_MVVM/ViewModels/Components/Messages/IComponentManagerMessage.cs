using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Messages
{
    public interface IComponentManagerMessage : IMessage
    {
        void Process(ComponentManager componentManager);
    }
}