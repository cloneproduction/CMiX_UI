using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
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


        public Component CreateComponent(ComponentMessageSender messageSender)
        {
            var model = new CompositionModel(Guid.NewGuid());
            var component = new Composition(ParentProject, model);
            component.SetAsSender(messageSender);
            ID++;
            return component;
        }


        public Component CreateComponent(ComponentMessageSender messageSender, IComponentModel model)
        {
            var component = new Composition(ParentProject, model as CompositionModel);
            component.SetAsSender(messageSender);
            return component;
        }


        public Component CreateComponent(ComponentMessageReceiver messageDispatcherReceiver)
        {
            var model = new CompositionModel(Guid.NewGuid());
            var component = new Composition(ParentProject, model);
            component.SetAsReceiver(messageDispatcherReceiver);
            ID++;
            return component;
        }
    }
}