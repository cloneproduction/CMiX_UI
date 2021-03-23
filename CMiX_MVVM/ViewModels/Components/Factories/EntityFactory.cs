using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class EntityFactory : IComponentFactory
    {
        public EntityFactory()
        {

        }

        private static int ID = 0;

        public Component CreateComponent(Component parentComponent, MessageDispatcher messageDispatcher)
        {
            return (parentComponent is Scene) ? CreateEntity((Scene)parentComponent, messageDispatcher) : null;
        }

        public Component CreateComponent(Component parentComponent, MessageDispatcher messageDispatcher, IComponentModel model)
        {
            return (parentComponent is Scene) ? CreateEntity((Scene)parentComponent, messageDispatcher, model as EntityModel) : null;
        }

        private Entity CreateEntity(Scene parentScene, MessageDispatcher messageDispatcher)
        {
            EntityModel entityModel = new EntityModel(ID);
            var component = new Entity(messageDispatcher, parentScene, entityModel);
            ID++;
            return component;
        }

        private Entity CreateEntity(Scene parentScene, MessageDispatcher messageDispatcher, EntityModel entityModel)
        {
            return new Entity(messageDispatcher, parentScene, entityModel as EntityModel);
        }
    }
}
