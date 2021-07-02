// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;

namespace CMiX.Core.Presentation.ViewModels.Components
{
    public class ComponentDatabase : IComponentDatabase
    {
        public ComponentDatabase()
        {
            Components = new Dictionary<Guid, Component>();
        }

        private Dictionary<Guid, Component> Components { get; set; }

        public void AddComponent(Component component)
        {
            if (!Components.ContainsKey(component.ID))
                Components.Add(component.ID, component);
        }

        public void RemoveComponent(Component component)
        {
            Components.Remove(component.ID);
        }

        public Component GetComponent(Guid id)
        {
            if (Components.ContainsKey(id))
                return Components[id];
            return null;
        }
    }
}
