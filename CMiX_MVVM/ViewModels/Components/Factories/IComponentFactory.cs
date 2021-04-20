using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public interface IComponentFactory
    {
        Component CreateComponent();
        Component CreateComponent(IComponentModel model);
    }
}