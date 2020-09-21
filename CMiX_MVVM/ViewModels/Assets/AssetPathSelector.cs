using GongSolutions.Wpf.DragDrop;
using System.Windows;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class AssetPathSelector : Sendable, IDropTarget
    {
        public AssetPathSelector(Asset defaultAsset, Sendable parentSendable)
        {
            SelectedAsset = defaultAsset;
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as AssetPathSelectorModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        private IAssets _selectedAsset;
        public IAssets SelectedAsset
        {
            get => _selectedAsset;
            set
            {
                SetAndNotify(ref _selectedAsset, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
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
    }
}