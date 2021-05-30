using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using GongSolutions.Wpf.DragDrop;
using System.Windows;

namespace CMiX.MVVM.ViewModels.Assets
{
    public class AssetPathSelector : Control, IDropTarget
    {
        public AssetPathSelector(Asset defaultAsset, AssetPathSelectorModel assetPathSelectorModel) 
        {
            this.ID = assetPathSelectorModel.ID;
            SelectedAsset = defaultAsset;
        }

        public AssetSelectorMessageEmitter AssetSelectorMessageEmitter { get; set; }

        public override void SetReceiver(IMessageReceiver messageReceiver)
        {
            var messageProcessor = new AssetSelectorMessageProcessor(this);
            var receiver = new MessageReceiver(messageProcessor);
            messageReceiver.RegisterReceiver(receiver);
        }

        public override void SetSender(IMessageSender messageSender)
        {
            var sender = new MessageSender(this);
            sender.SetSender(messageSender);
            AssetSelectorMessageEmitter = new AssetSelectorMessageEmitter(sender);
        }


        private Asset _selectedAsset;
        public Asset SelectedAsset
        {
            get => _selectedAsset;
            set
            {
                SetAndNotify(ref _selectedAsset, value);
                AssetSelectorMessageEmitter?.SendMessageAsset(value);
                if (value != null)
                    System.Console.WriteLine("SelectedAsset Name is " + SelectedAsset.Name);
            }
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.DragInfo != null && dropInfo.DragInfo.SourceItem != null)
                dropInfo.Effects = DragDropEffects.Copy;
        }

        public void Drop(IDropInfo dropInfo)
        {
            //SelectedPath = ((IAssets)dropInfo.DragInfo.SourceItem).Path;
        }

        public override void SetViewModel(IModel model)
        {
            AssetPathSelectorModel assetPathSelectorModel = model as AssetPathSelectorModel;
            assetPathSelectorModel.ID = this.ID;

            if (this.SelectedAsset == null)
            {
                if (model is AssetTextureModel)
                    this.SelectedAsset = new AssetTexture();
                else if (model is AssetGeometryModel)
                    this.SelectedAsset = new AssetGeometry();
            }

            if (assetPathSelectorModel.SelectedAsset != null)
                this.SelectedAsset.SetViewModel(assetPathSelectorModel.SelectedAsset);
        }

        public override IModel GetModel()
        {
            AssetPathSelectorModel model = new AssetPathSelectorModel();
            model.ID = this.ID;
            if (this.SelectedAsset != null)
                model.SelectedAsset = (IAssetModel)this.SelectedAsset.GetModel();
            return model;
        }
    }
}