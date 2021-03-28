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


        private static int ID = 0;
        private Scene ParentScene { get; set; }


        public Component CreateComponent()
        {
            EntityModel entityModel = new EntityModel(Guid.NewGuid());
            var component = new Entity(ParentScene, entityModel);
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            return new Entity(ParentScene, model as EntityModel);
        }
    }
}
