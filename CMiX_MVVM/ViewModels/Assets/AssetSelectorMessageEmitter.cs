using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.ViewModels.Assets
{
    public class AssetSelectorMessageEmitter : IMessageEmitter
    {
        public AssetSelectorMessageEmitter(IMessageSender messageSender)
        {
            MessageSender = messageSender;
        }

        IMessageSender MessageSender { get; set; }

        public void SendMessageAsset(Asset asset)
        {
            Console.WriteLine("AssetSelectorMessageEmitter SendMessageAsset");
            var message = new MessageAsset(asset.GetModel() as IAssetModel);
            MessageSender.SendMessage(message);
        }
    }
}