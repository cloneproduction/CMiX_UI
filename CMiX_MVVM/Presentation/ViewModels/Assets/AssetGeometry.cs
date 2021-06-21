﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;

namespace CMiX.Core.Presentation.ViewModels.Assets
{
    public class AssetGeometry : Asset
    {
        public AssetGeometry()
        {

        }

        public AssetGeometry(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public override IModel GetModel()
        {
            var assetModel = new AssetGeometryModel();

            assetModel.Name = this.Name;
            assetModel.Path = this.Path;
            assetModel.Ponderation = this.Ponderation;

            return assetModel;
        }

        public override void SetViewModel(IModel model)
        {
            AssetGeometryModel assetGeometryModel = model as AssetGeometryModel;
            this.Name = assetGeometryModel.Name;
            this.Path = assetGeometryModel.Path;
            this.Ponderation = assetGeometryModel.Ponderation;
        }
    }
}
