using CMiX.Core.Models;
using CMiX.Core.Network.Messages;
using CMiX.Core.Presentation.ViewModels.Assets;

namespace CMiX.Core.Network.Communicators
{
    public class AssetPathSelectorCommunicator : Communicator
    {
        public AssetPathSelectorCommunicator(AssetPathSelector assetPathSelector) : base()
        {
            IIDObject = assetPathSelector;
        }

        public void SendMessageSelectedAsset(Asset selectedAsset)
        {
            this.SendMessage(new MessageAsset(selectedAsset.GetModel() as IAssetModel));
        }
    }
}
