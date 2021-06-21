﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.ObjectModel;

namespace CMiX.Core.Models
{
    public interface IComponentModel : IModel
    {
        Guid ID { get; set; }
        string Name { get; set; }
        bool IsVisible { get; set; }
        ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}