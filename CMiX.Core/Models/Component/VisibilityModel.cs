// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models.Component
{
    public class VisibilityModel : Model, IModel
    {
        public VisibilityModel()
        {
            this.ID = Guid.NewGuid();
        }

        public bool IsVisible { get; set; }
    }
}
