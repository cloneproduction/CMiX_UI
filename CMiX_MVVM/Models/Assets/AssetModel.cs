﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.ObjectModel;

namespace CMiX.Core.Models
{
    public class AssetModel : IAssetModel
    {
        public AssetModel()
        {

        }

        public string Name { get; set; }
        public string Ponderation { get; set; }
        public string Path { get; set; }

        public ObservableCollection<IAssetModel> AssetModels { get ; set; }
        public bool Enabled { get; set; }
    }
}
