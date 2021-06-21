// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using System;

namespace CMiX.Core.Presentation.ViewModels.Components.Factories
{
    public class SceneFactory : IComponentFactory
    {
        public SceneFactory(Layer parentLayer)
        {
            ParentLayer = parentLayer;
        }

        private static int ID = 0;
        private Layer ParentLayer { get; set; }


        public Component CreateComponent()
        {
            var model = new SceneModel(Guid.NewGuid());
            var component = new Scene(ParentLayer, model);
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            var component = new Scene(ParentLayer, model as SceneModel);
            return component;
        }
    }
}