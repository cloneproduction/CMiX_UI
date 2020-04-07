using System.Windows;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using Memento;
using GongSolutions.Wpf.DragDrop;

namespace CMiX.Studio.ViewModels
{
    public class AssetSelector : ViewModel, ISendable, IUndoable, IDropTarget
    {
        public AssetSelector(string messageAddress, MessageService messageService, Mementor mementor)
        {
            MessageAddress = $"{messageAddress}{nameof(AssetSelector)}/";
            MessageService = messageService;
            Mementor = mementor;
        }

        public Mementor Mementor { get; set; }
        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }

        private Asset _selectedAsset;
        public Asset SelectedAsset
        {
            get => _selectedAsset;
            set => SetAndNotify(ref _selectedAsset, value);
        }

        public AssetSelectorModel GetModel()
        {
            AssetSelectorModel model = new AssetSelectorModel();

            if(SelectedAsset != null)
                model.SelectedAssetModel = this.SelectedAsset.GetModel();

            return model;
        }

        public void SetViewModel(AssetSelectorModel model)
        {
            MessageService.Disable();

            if(model.SelectedAssetModel != null)
            {
                SelectedAsset = new Asset();
                SelectedAsset.SetViewModel(model.SelectedAssetModel);
                System.Console.WriteLine("SetViewModel(AssetSelectorModel model)");
            }

            MessageService.Enable();
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if(dropInfo.DragInfo != null)
            {
                if (dropInfo.DragInfo.SourceItem != null && dropInfo.DragInfo.SourceItem is IAssets)
                {
                    dropInfo.Effects = DragDropEffects.Copy;
                }
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            SelectedAsset = dropInfo.DragInfo.SourceItem as Asset;
        }
    }
}
