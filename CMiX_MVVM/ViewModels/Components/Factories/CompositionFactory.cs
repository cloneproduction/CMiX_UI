using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class CompositionFactory : IComponentFactory
    {
        public CompositionFactory(MessageTerminal MessageTerminal)
        {
            this.MessageTerminal = MessageTerminal;
        }

        private static int ID = 0;

        private MessageTerminal MessageTerminal { get; set; }

        public Component CreateComponent(Component parentComponent)
        {
            return CreateComposition((Project)parentComponent);
        }

        public Component CreateComponent(Component parentComponent, IComponentModel model)
        {
            return CreateComposition((Project)parentComponent, model);
        }

        private Composition CreateComposition(Project parentComponent)
        {
            var component = new Composition(ID, MessageTerminal, parentComponent);
            ID++;
            return component;
        }

        private Composition CreateComposition(Project parentComponent, IComponentModel componentModel)
        {
            var component = new Composition(componentModel.ID, MessageTerminal, parentComponent);
            component.SetViewModel(componentModel);
            return component;
        }
    }
}
