// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Presentation.ViewModels;

namespace CMiX.Core.Interfaces
{
    public interface IModifier
    {
        string Name { get; set; }

        AnimParameter X { get; set; }
        AnimParameter Y { get; set; }
        AnimParameter Z { get; set; }
    }
}
