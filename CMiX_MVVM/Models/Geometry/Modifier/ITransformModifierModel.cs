// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using CMiX.Core.Presentation.ViewModels;
using System;

namespace CMiX.Core.Models
{
    public interface ITransformModifierModel : IModel
    {
        TransformModifierNames Name { get; set; }
        Guid ID { get; set; }
        int Count { get; set; }
    }
}
