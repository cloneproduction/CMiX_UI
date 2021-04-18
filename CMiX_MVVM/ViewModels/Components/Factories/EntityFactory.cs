using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.ModuleMessenger;
using System;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class EntityFactory : IComponentFactory
    {
        public EntityFactory(Scene parentScene)
        {
            ParentScene = parentScene;
        }

        private static int ID = 0;
        private Scene ParentScene { get; set; }


        public Component CreateComponent(ComponentMessageReceiver messageReceiver)
        {
            EntityModel entityModel = new EntityModel(Guid.NewGuid());
            var component = new Entity(ParentScene, entityModel);
            component.SetAsReceiver(messageReceiver);
            ID++;
            return component;
        }

        public Component CreateComponent(ComponentMessageReceiver messageReceiver, IComponentModel model)
        {
            var component = new Entity(ParentScene, model as EntityModel);
            component.SetAsReceiver(messageReceiver);
            return component;
        }


        public Component CreateComponent(ComponentMessageSender messageSender)
        {
            EntityModel entityModel = new EntityModel(Guid.NewGuid());
            var component = new Entity(ParentScene, entityModel);
            component.SetAsSender(messageSender);
            ID++;
            return component;
        }

        public Component CreateComponent(ComponentMessageSender messageSender, IComponentModel model)
        {
            var component = new Entity(ParentScene, model as EntityModel);
            component.SetAsSender(messageSender);
            return component;
        }
    }
}
