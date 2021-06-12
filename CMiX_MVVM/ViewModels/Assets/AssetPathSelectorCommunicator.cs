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

        public void SetCommunicator(ICommunicator communicator)
        {
            var messageProcessor = new AssetSelectorMessageProcessor(AssetPathSelector);
            MessageReceiver = new MessageReceiver(messageProcessor);
            communicator.MessageReceiver?.RegisterReceiver(MessageReceiver);

            MessageSender = new MessageSender(AssetPathSelector);
            MessageSender.SetSender(communicator.MessageSender);
        }

        public void UnsetCommunicator(ICommunicator communicator)
        {
            communicator.MessageReceiver?.UnregisterReceiver(MessageReceiver);
        }


        public void SendMessageSelectedAsset(Asset selectedAsset)
        {
            MessageSender?.SendMessage(new MessageAsset(selectedAsset.GetModel() as IAssetModel));
        }

        public void SendMessage<T>(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
