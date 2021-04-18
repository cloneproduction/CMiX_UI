using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public interface IComponentFactory
    {
        Component CreateComponent(IMessageDispatcher messageDispatcher);
        Component CreateComponent(IMessageDispatcher messageDispatcher, IComponentModel model);
    }
}