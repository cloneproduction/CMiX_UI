// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models
{
    public abstract class Model : IModel
    {
        public Guid ID { get; set; }
        public bool Enabled { get; set; }
    }
}
