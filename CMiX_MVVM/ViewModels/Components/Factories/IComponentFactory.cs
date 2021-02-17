using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public interface IComponentFactory
    {
        Component CreateComponent(Component parentComponent);
        Component CreateComponent(Component parentComponent, IComponentModel model);
        MessageTerminal MessageTerminal { get; set; }
    }
}