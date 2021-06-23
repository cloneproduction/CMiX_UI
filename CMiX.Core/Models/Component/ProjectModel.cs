// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using System;
using System.Collections.ObjectModel;

namespace CMiX.Core.Models
{
    public class ProjectModel : IComponentModel
    {
        public ProjectModel()
        {
            ID = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00");
            ComponentModels = new ObservableCollection<IComponentModel>();
            AssetModels = new ObservableCollection<IAssetModel>();
            AssetModelsFlatten = new ObservableCollection<IAssetModel>();
        }

        public bool Enabled { get; set; }
        public string Address { get; set; }
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }

        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
        public ObservableCollection<IAssetModel> AssetModels { get; set; }
        public ObservableCollection<IAssetModel> AssetModelsFlatten { get; set; }
    }
}
