using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
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


        public Component CreateComponent(IMessageDispatcher messageReceiver)
        {
            EntityModel entityModel = new EntityModel(Guid.NewGuid());
            Component component = new Entity(ParentScene, entityModel);
            component.SetMessageCommunication(messageReceiver);
            ID++;
            return component;
        }

        public Component CreateComponent(IMessageDispatcher messageReceiver, IComponentModel model)
        {
            var component = new Entity(ParentScene, model as EntityModel);
            component.SetMessageCommunication(messageReceiver);
            return component;
        }
    }
}
