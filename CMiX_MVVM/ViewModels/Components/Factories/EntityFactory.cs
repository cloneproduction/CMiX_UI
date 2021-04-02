using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class EntityFactory : IComponentFactory
    {
        public EntityFactory(Scene parentScene, IMessageDispatcher messageDispatcher)
        {
            MessageDispatcher = messageDispatcher;
            ParentScene = parentScene;
        }

        private IMessageDispatcher MessageDispatcher { get; set; }
        private static int ID = 0;
        private Scene ParentScene { get; set; }


        public Component CreateComponent()
        {
            EntityModel entityModel = new EntityModel(Guid.NewGuid());
            var component = new Entity(ParentScene, entityModel, MessageDispatcher);
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            return new Entity(ParentScene, model as EntityModel, MessageDispatcher);
        }
    }
}
