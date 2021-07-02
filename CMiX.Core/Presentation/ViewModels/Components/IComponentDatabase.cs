// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace CMiX.Core.Presentation.ViewModels.Components
{
    public interface IComponentDatabase
    {
        void AddComponent(Component component);
        void RemoveComponent(Component component);
        Component GetComponent(Guid id);
    }
}
