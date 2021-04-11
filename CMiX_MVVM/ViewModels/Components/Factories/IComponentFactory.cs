using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public interface IComponentFactory
    {
        Component CreateComponent(IMessageDispatcherSender messageDispatcher);
        Component CreateComponent(IMessageDispatcherSender messageDispatcher, IComponentModel model);
    }
}