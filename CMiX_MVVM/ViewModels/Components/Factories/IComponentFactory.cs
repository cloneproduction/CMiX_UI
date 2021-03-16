using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public interface IComponentFactory
    {
        Component CreateComponent(Component parentComponent);
        Component CreateComponent(Component parentComponent, IComponentModel model);
    }
}