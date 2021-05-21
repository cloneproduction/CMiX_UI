using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Assets;
using System;

namespace CMiX.MVVM.MessageService
{
    public class AssetSelectorMessageProcessor : IMessageProcessor
    {
        public AssetSelectorMessageProcessor(AssetPathSelector assetPathSelector)
        {
            AssetPathSelector = assetPathSelector;
        }

        private AssetPathSelector AssetPathSelector { get; set; }

        public void ProcessMessage(Message message)
        {
            Console.WriteLine("AssetSelectorMessageProcessor ProcessMessage");

            var msg = message as IAssetPathSelectorMessage;

            if (msg != null)
            {
                var model = msg.AssetModel;

                if(model is AssetGeometryModel)
                {
                    var asset = new AssetGeometry();
                    asset.SetViewModel(model);
                    AssetPathSelector.SelectedAsset = asset;
                    Console.WriteLine(AssetPathSelector.SelectedAsset.Path);
                }

                if(model is AssetTextureModel)
                {
                    var asset = new AssetTexture();
                    asset.SetViewModel(model);
                    AssetPathSelector.SelectedAsset = asset;
                }
            }
        }
    }
}