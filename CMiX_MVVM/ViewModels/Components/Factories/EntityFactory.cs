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


        public Component CreateComponent()
        {
            EntityModel entityModel = new EntityModel(Guid.NewGuid());
            var messageDispatcher = new MessageDispatcher();
            var component = new Entity(ParentScene, entityModel, messageDispatcher);
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            var messageDispatcher = new MessageDispatcher();
            return new Entity(ParentScene, model as EntityModel, messageDispatcher);
        }
    }
}
