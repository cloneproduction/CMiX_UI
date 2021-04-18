using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
using System;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class SceneFactory : IComponentFactory
    {
        public SceneFactory(Layer parentLayer)
        {
            ParentLayer = parentLayer;
        }

        private static int ID = 0;
        private Layer ParentLayer { get; set; }


        public Component CreateComponent(ComponentMessageReceiver messageDispatcher)
        {
            var model = new SceneModel(Guid.NewGuid());
            var component = new Scene(ParentLayer, model);
            component.SetAsReceiver(messageDispatcher);
            ID++;
            return component;
        }

        public Component CreateComponent(ComponentMessageReceiver messageDispatcher, IComponentModel model)
        {
            var component = new Scene(ParentLayer, model as SceneModel);
            component.SetAsReceiver(messageDispatcher);
            return component;
        }


        public Component CreateComponent(ComponentMessageSender messageDispatcher)
        {
            var model = new SceneModel(Guid.NewGuid());
            var component = new Scene(ParentLayer, model);
            component.SetAsSender(messageDispatcher);
            ID++;
            return component;
        }

        public Component CreateComponent(ComponentMessageSender messageDispatcher, IComponentModel model)
        {
            var component = new Scene(ParentLayer, model as SceneModel);
            component.SetAsSender(messageDispatcher);
            return component;
        }
    }
}