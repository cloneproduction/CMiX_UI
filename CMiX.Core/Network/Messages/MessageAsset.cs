﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using CMiX.Core.Presentation.ViewModels.Assets;
using System;

namespace CMiX.Core.Network.Messages
{
    public class MessageAsset : Message
    {
        public MessageAsset()
        {

        }

        public MessageAsset(IAssetModel assetModel)
        {
            AssetModel = assetModel;
        }

        public IAssetModel AssetModel { get; set; }

        public override void Process<T>(T receiver)
        {
            Console.WriteLine("AssetSelectorMessageProcessor ProcessMessage");

            AssetPathSelector assetPathSelector = receiver as AssetPathSelector;
            if (AssetModel is AssetGeometryModel)
            {
                var asset = new AssetGeometry();
                asset.SetViewModel(AssetModel);
                assetPathSelector.SelectedAsset = asset;
                Console.WriteLine(assetPathSelector.SelectedAsset.Path);
            }

            if (AssetModel is AssetTextureModel)
            {
                var asset = new AssetTexture();
                asset.SetViewModel(AssetModel);
                assetPathSelector.SelectedAsset = asset;
                Console.WriteLine(assetPathSelector.SelectedAsset.Path);
            }
        }
    }
}