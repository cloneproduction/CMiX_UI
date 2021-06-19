using CMiX.Core.Interfaces;

namespace CMiX.Core.Presentation.ViewModels.Components.Factories
{
    public interface IComponentFactory
    {
        Component CreateComponent();
        Component CreateComponent(IComponentModel model);
    }
}