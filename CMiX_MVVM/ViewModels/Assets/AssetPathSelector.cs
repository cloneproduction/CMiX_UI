using GongSolutions.Wpf.DragDrop;
using System.Windows;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class AssetPathSelector : Sender, IDropTarget
    {
        public AssetPathSelector(string name, IColleague parentSender, Asset defaultAsset) : base(name, parentSender)
        {
            SelectedAsset = defaultAsset;
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as AssetPathSelectorModel);
        }

        private IAssets _selectedAsset;
        public IAssets SelectedAsset
        {
            get => _selectedAsset;
            set
            {
                SetAndNotify(ref _selectedAsset, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
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