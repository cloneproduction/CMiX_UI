using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public interface IComponentFactory
    {
        Component CreateComponent(ComponentMessageReceiver messageDispatcher);
        Component CreateComponent(ComponentMessageReceiver messageDispatcher, IComponentModel model);
    }
}