using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class CompositionFactory : IComponentFactory
    {
        public CompositionFactory(MessengerTerminal messengerTerminal)
        {
            this.MessengerTerminal = messengerTerminal;
        }

        private static int ID = 0;

        public MessengerTerminal MessengerTerminal { get; set; }

        public IComponent CreateComponent(IComponent parentComponent)
        {
            return CreateComposition((Project)parentComponent);
        }

        public IComponent CreateComponent(IComponent parentComponent, IComponentModel model)
        {
            return CreateComposition((Project)parentComponent, model);
        }

        private Composition CreateComposition(Project parentComponent)
        {
            var component = new Composition(ID, MessengerTerminal);
            ID++;
            return component;
        }

        private Composition CreateComposition(Project parentComponent, IComponentModel componentModel)
        {
            var component = new Composition(componentModel.ID, MessengerTerminal);
            component.SetViewModel(componentModel);
            return component;
        }
    }
}
