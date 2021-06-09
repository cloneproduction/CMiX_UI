using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class EntityFactory : IComponentFactory
    {
        public EntityFactory(Scene parentScene)
        {
            ParentScene = parentScene;
        }

        private Scene ParentScene { get; set; }

        public Component CreateComponent()
        {
            EntityModel entityModel = new EntityModel(Guid.NewGuid());
            Component component = new Entity(ParentScene, entityModel);
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            var component = new Entity(ParentScene, model as EntityModel);
            return component;
        }
    }
}
