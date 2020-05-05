using Memento;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public class AssetPathSelector<T> : ViewModel, IDropTarget
    {
        public AssetPathSelector()
        {
            
        }

        private string _selectedPath;
        public string SelectedPath
        {
            get => _selectedPath;
            set => SetAndNotify(ref _selectedPath, value);
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

        public void SetViewModel(AssetPathSelectorModel model)
        {
            if (model.SelectedPath != null)
                SelectedPath = model.SelectedPath;
        }
    }
}