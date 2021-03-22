using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
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
            return CreateComposition((Project)parentComponent, model as CompositionModel);
        }



        private Composition CreateComposition(Project parentComponent)
        {
            var model = new CompositionModel(ID);
            var component = new Composition(MessageTerminal, parentComponent, model);
            ID++;
            return component;
        }

        private Composition CreateComposition(Project parentComponent, CompositionModel compositionModel)
        {
            return new Composition(MessageTerminal, parentComponent, compositionModel);
        }
    }
}
