using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class CompositionFactory : IComponentFactory
    {
        public CompositionFactory(Project parentProject, IMessageDispatcher messageDispatcher)
        {
            MessageDispatcher = messageDispatcher;
            ParentProject = parentProject;
        }

        private IMessageDispatcher MessageDispatcher { get; set; }
        private Project ParentProject { get; set; }

        private static int ID = 0;

        public Component CreateComponent()
        {
            var model = new CompositionModel(Guid.NewGuid());
            var component = new Composition(ParentProject, model, MessageDispatcher);
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            return new Composition(ParentProject, model as CompositionModel, MessageDispatcher);
        }
    }
}
