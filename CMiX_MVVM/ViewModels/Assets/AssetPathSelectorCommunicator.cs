using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.ViewModels.Assets
{
    public class AssetPathSelectorCommunicator : ICommunicator
    {
        public AssetPathSelectorCommunicator(AssetPathSelector assetPathSelector)
        {
            AssetPathSelector = assetPathSelector;
        }

        private AssetPathSelector AssetPathSelector { get; set; }
        public IMessageReceiver MessageReceiver { get; set; }
        public IMessageSender MessageSender { get; set; }

        public void SetNextCommunicator(ICommunicator communicator)
        {
            var messageProcessor = new AssetSelectorMessageProcessor(AssetPathSelector);
        }

        public void SendMessage(AssetPathSelector assetPathSelector)
        {
            MessageSender?.SendMessage(new MessageAsset(assetPathSelector.SelectedAsset.GetModel() as IAssetModel));
        }

        public void SendMessage<T>(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
