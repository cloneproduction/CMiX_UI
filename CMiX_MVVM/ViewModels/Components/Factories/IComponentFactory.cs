using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public interface IComponentFactory
    {
        IComponent CreateComponent(IComponent parentComponent);
        IComponent CreateComponent(IComponent parentComponent, IComponentModel model);
        MessengerTerminal MessengerTerminal { get; set; }
    }
}