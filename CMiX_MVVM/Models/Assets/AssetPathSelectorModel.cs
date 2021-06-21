// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Core.Models
{
    public class AssetPathSelectorModel : Model, IModel
    {
        public AssetPathSelectorModel()
        {
            this.ID = Guid.NewGuid();
        }
        public IAssetModel SelectedAsset { get; set; }
    }
}
