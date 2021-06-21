﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using System;
using System.Collections.ObjectModel;

namespace CMiX.Core.Models
{
    public class ComponentModel : Model, IComponentModel
    {
        public ComponentModel()
        {
            ComponentModels = new ObservableCollection<IComponentModel>();
        }

        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public bool IsVisible { get; set; }
        public bool ParentIsVisible { get; set; }
        public bool IsExpanded { get; set; }
        public string Address { get; set; }
        public ComponentModel SelectedComponent { get; set; }
        public string MessageAddress { get; set; }
        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}
