using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using GongSolutions.Wpf.DragDrop;
using System.Windows;

namespace CMiX.MVVM.ViewModels
{
    public class AssetPathSelector : Sender, IDropTarget
    {
        public AssetPathSelector(string name, IMessageProcessor parentSender, Asset defaultAsset) : base(name, parentSender)
        {
            SelectedAsset = defaultAsset;
        }

        private IAssets _selectedAsset;
        public IAssets SelectedAsset
        {
            get => _selectedAsset;
            set
            {
                SetAndNotify(ref _selectedAsset, value);
                this.MessageDispatcher.NotifyOut(new MessageUpdateViewModel(this.GetAddress(), this.GetModel()));
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
            if (this.SelectedAsset != null)
                model.SelectedAsset = this.SelectedAsset.GetModel();

            return model;
        }
    }
}