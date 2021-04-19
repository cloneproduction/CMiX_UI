using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class CompositionFactory : IComponentFactory
    {
        public CompositionFactory(Project parentProject)
        {
            ParentProject = parentProject;
        }


        private Project ParentProject { get; set; }
        private static int ID = 0;


        public Component CreateComponent(IMessageDispatcher messageDispatcher)
        {
            var model = new CompositionModel(Guid.NewGuid());
            var component = new Composition(ParentProject, model);
            component.SetMessageCommunication(messageDispatcher);
            ID++;
            return component;
        }


        public Component CreateComponent(IMessageDispatcher messageDispatcher, IComponentModel model)
        {
            var component = new Composition(ParentProject, model as CompositionModel);
            component.SetMessageCommunication(messageDispatcher);
            return component;
        }
    }
}