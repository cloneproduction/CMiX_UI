using GongSolutions.Wpf.DragDrop;
using System.Windows;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class AssetPathSelector<T> : Sendable, IDropTarget
    {
        public AssetPathSelector()
        {
            
        }

        public AssetPathSelector(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as AssetPathSelectorModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        private string _selectedPath;
        public string SelectedPath
        {
            get => _selectedPath;
            set
            {
                SetAndNotify(ref _selectedPath, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.DragInfo != null)
            {
                if (dropInfo.DragInfo.SourceItem != null && dropInfo.DragInfo.SourceItem is T)
                {
                    dropInfo.Effects = DragDropEffects.Copy;
                }
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            SelectedPath = ((IAssets)dropInfo.DragInfo.SourceItem).Path;
        }

        public AssetPathSelectorModel GetModel()
        {
            AssetPathSelectorModel model = new AssetPathSelectorModel();
            model.SelectedPath = this.SelectedPath;
            return model;
        }

        public void SetViewModel(AssetPathSelectorModel model)
        {
            this.SelectedPath = model.SelectedPath;
            //if (model.SelectedPath != null)
            //    SelectedPath = model.SelectedPath;
        }
    }
}