using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
using GongSolutions.Wpf.DragDrop;
using System.Windows;

namespace CMiX.MVVM.ViewModels.Assets
{
    public class AssetPathSelector : Module, IDropTarget
    {
        public AssetPathSelector(Asset defaultAsset, AssetPathSelectorModel assetPathSelectorModel) 
        {
            SelectedAsset = defaultAsset;
        }

        public override void SetReceiver(ModuleMessageReceiver messageDispatcher)
        {
            messageDispatcher.RegisterReceiver(this);
        }


        private Asset _selectedAsset;
        public Asset SelectedAsset
        {
            get => _selectedAsset;
            set
            {
                SetAndNotify(ref _selectedAsset, value);

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
                model.SelectedAsset = (IAssetModel)this.SelectedAsset.GetModel();
            return model;
        }
    }
}