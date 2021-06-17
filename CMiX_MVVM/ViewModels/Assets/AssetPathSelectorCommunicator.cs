using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels.Assets
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
