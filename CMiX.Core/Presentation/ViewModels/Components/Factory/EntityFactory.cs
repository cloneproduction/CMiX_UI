﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using System;

namespace CMiX.Core.Presentation.ViewModels.Components.Factories
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
