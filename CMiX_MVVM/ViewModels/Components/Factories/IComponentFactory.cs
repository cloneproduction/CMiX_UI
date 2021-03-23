using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public interface IComponentFactory
    {
        Component CreateComponent(Component parentComponent, MessageDispatcher messageDispatcher);
        Component CreateComponent(Component parentComponent, MessageDispatcher messageDispatcher, IComponentModel model);
    }
}