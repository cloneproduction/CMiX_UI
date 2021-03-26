using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class CompositionFactory : IComponentFactory
    {
        public CompositionFactory(Project parentProject, IMessageTerminal messageTerminal)
        {
            ParentProject = parentProject;
            MessageTerminal = messageTerminal;
        }

        private Project ParentProject { get; set; }
        private IMessageTerminal MessageTerminal { get; set; }

        private static int ID = 0;

        public Component CreateComponent()
        {
            var model = new CompositionModel(ID);
            var component = new Composition(MessageTerminal, ParentProject, model);
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            return new Composition(MessageTerminal, ParentProject, model as CompositionModel);
        }
    }
}
