using CMiX.MVVM.Models;

namespace CMiX.MVVM.MessageService
{
    public class MessageAsset : Message, IAssetPathSelectorMessage
    {
        public MessageAsset()
        {

        }

        public MessageAsset(IAssetModel assetModel)
        {
            AssetModel = assetModel;
        }

        public IAssetModel AssetModel { get; set; }
    }
}