using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class CompositionFactory : IComponentFactory
    {
        public CompositionFactory()
        {

        }

        private static int ID = 0;

        public Component CreateComponent(Component parentComponent, MessageDispatcher messageDispatcher)
        {
            return CreateComposition((Project)parentComponent, messageDispatcher);
        }

        public Component CreateComponent(Component parentComponent, MessageDispatcher messageDispatcher, IComponentModel model)
        {
            return CreateComposition((Project)parentComponent, messageDispatcher, model as CompositionModel);
        }



        private Composition CreateComposition(Project parentProject, MessageDispatcher messageDispatcher)
        {
            var model = new CompositionModel(ID);
            var component = new Composition(messageDispatcher, parentProject, model);
            ID++;
            return component;
        }

        private Composition CreateComposition(Project parentProject, MessageDispatcher messageDispatcher, CompositionModel compositionModel)
        {
            return new Composition(messageDispatcher, parentProject, compositionModel);
        }
    }
}
