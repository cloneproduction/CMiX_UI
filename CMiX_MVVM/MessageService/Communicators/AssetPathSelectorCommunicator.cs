using CMiX.Core.MessageService;
using CMiX.Core.Models;

namespace CMiX.Core.Presentation.ViewModels.Assets
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
