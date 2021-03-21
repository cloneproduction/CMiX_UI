﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class EntityFactory : IComponentFactory
    {
        public EntityFactory(MessageTerminal MessageTerminal)
        {
            this.MessageTerminal = MessageTerminal;
        }

        private static int ID = 0;

        private MessageTerminal MessageTerminal { get; set; }

        public Component CreateComponent(Component parentComponent)
        {
            return (parentComponent is Scene) ? CreateEntity((Scene)parentComponent) : null;
        }

        public Component CreateComponent(Component parentComponent, IComponentModel model)
        {
            return (parentComponent is Scene) ? CreateEntity((Scene)parentComponent, model as EntityModel) : null;
        }

        private Entity CreateEntity(Scene parentScene)
        {
            EntityModel entityModel = new EntityModel(ID);
            var component = new Entity(MessageTerminal, parentScene, entityModel);
            ID++;
            return component;
        }

        private Entity CreateEntity(Scene parentScene, EntityModel entityModel)
        {
            var component = new Entity(MessageTerminal, parentScene, entityModel as EntityModel);
            return component;
        }
    }
}
