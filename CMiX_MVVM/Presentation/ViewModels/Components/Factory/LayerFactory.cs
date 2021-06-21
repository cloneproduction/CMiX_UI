﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using System;

namespace CMiX.Core.Presentation.ViewModels.Components.Factories
{
    public class LayerFactory : IComponentFactory
    {
        public LayerFactory(Composition parentComposition)
        {
            ParentComposition = parentComposition;
        }

        private static int ID = 0;
        public Composition ParentComposition { get; set; }


        public Component CreateComponent()
        {
            var model = new LayerModel(Guid.NewGuid());
            var component = new Layer(ParentComposition, model);
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            var component = new Layer(ParentComposition, model as LayerModel);
            return component;
        }
    }
}
