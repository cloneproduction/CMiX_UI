using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.ViewModels;
using GongSolutions.Wpf.DragDrop;
using System.Linq;

namespace CMiX.Studio.ViewModels
{
    public class Assets : ViewModel, IDropTarget, IDragSource
    {
        public Assets()
        {
            GeometryItems = new ObservableCollection<GeometryItem>();
            TextureItems = new ObservableCollection<TextureItem>();
            ResourceItems = new ObservableCollection<IAssets>();

            var directoryItem = new RootItem() { IsSelected = true };
            ResourceItems.Add(directoryItem);
            SelectedItem = directoryItem;

            AddDirectoryItemCommand = new RelayCommand(p => AddDirectoryItem());
            DeleteSelectedItemCommand = new RelayCommand(p => DeleteSelectedItem());
            RenameSelectedItemCommand = new RelayCommand(p => RenameSelectedItem());
        }

        #region METHODS


        public void RenameSelectedItem()
        {
            if(SelectedItem != null)
                SelectedItem.IsRenaming = true;
        }

        private void DeleteSelectedItem()
        {
            if (SelectedItem != null)
            {
                DeleteItem(ResourceItems);
                UpdateTextureItem(ResourceItems);
            }
        }


        public void DeleteItem(ObservableCollection<IAssets> assets)
        {
            if (assets.Contains(SelectedItem))
            {
                assets.Remove(SelectedItem);
                return;
            }
            else
            {
                foreach (var asset in assets)
                {
                    DeleteItem(asset.Assets);
                }
            }
        }

        public void AddDirectoryItem()
        {
            var directoryItem = new DirectoryItem("NewFolder", null);
            if(SelectedItem is DirectoryItem)
            {
                var dir = SelectedItem as DirectoryItem;
                dir.Assets.Add(directoryItem);
            }
            else if (SelectedItem is null && ResourceItems.Count > 0)
            {
                ResourceItems[0].Assets.Add(directoryItem);
            }
        }


        #endregion

        #region PROPERTIES
        public ICommand RenameSelectedItemCommand { get; set; }
        public ICommand AddDirectoryItemCommand { get; set; }
        public ICommand AddNewFolderCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand DeleteSelectedItemCommand { get; set; }

        private IAssets _selectedItem;
        public IAssets SelectedItem
        {
            get => _selectedItem;
            set => SetAndNotify(ref _selectedItem, value);
        }

        private ObservableCollection<IAssets> _resourceItems;
        public ObservableCollection<IAssets> ResourceItems
        {
            get => _resourceItems;
            set => SetAndNotify(ref _resourceItems, value);
        }

        private ObservableCollection<GeometryItem> _geometryItems;
        public ObservableCollection<GeometryItem> GeometryItems
        {
            get => _geometryItems;
            set => SetAndNotify(ref _geometryItems, value);
        }

        private ObservableCollection<TextureItem> _textureItems;
        public ObservableCollection<TextureItem> TextureItems
        {
            get => _textureItems;
            set => SetAndNotify(ref _textureItems, value);
        }

        private void UpdateTextureItem(ObservableCollection<IAssets> assets)
        {
            foreach (var asset in assets)
            {
                if (asset is TextureItem && !assets.Contains(asset))
                    TextureItems.Add(asset as TextureItem);
                else if (asset is DirectoryItem)
                    UpdateTextureItem(((DirectoryItem)asset).Assets);
            }
            Console.WriteLine("UpdateTextureItem");
        }

        private IAssets GetItemFromDirectory(DirectoryInfo directoryInfo)
        {
            var directoryItem = new DirectoryItem(directoryInfo.Name, directoryInfo.FullName);

            foreach (var directory in directoryInfo.GetDirectories())
                directoryItem.Assets.Add(GetItemFromDirectory(directory));

            foreach (var file in directoryInfo.GetFiles())
            {
                var item = GetFileItem(file.FullName);
                if (item != null)
                    directoryItem.Assets.Add(item);
            }
            return directoryItem;
        }

        private IAssets GetFileItem(string filePath)
        {
            string fileType = Path.GetExtension(filePath).ToUpper().TrimStart('.');
            string fileName = Path.GetFileName(filePath);

            if (fileType == TextureFileType.PNG.ToString() || fileType == TextureFileType.JPG.ToString() || fileType == TextureFileType.MOV.ToString())
                return new TextureItem(fileName, filePath);
            else if (fileType == GeometryFileType.FBX.ToString() || fileType == GeometryFileType.OBJ.ToString())
                return new GeometryItem(fileName, filePath);
            else
                return null;
        }
        #endregion

        #region DRAG DROP
        public void DragOver(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as DataObject;

            if (dataObject != null && dataObject.ContainsFileDropList())
                dropInfo.Effects = DragDropEffects.Copy;

            if (dropInfo.DragInfo != null)
            {
                var vSourceItem = dropInfo.DragInfo.VisualSourceItem as TreeViewItem;
                var vSourceChild = Utils.FindVisualChildren<TreeViewItem>(vSourceItem);
                var vTargetItem = dropInfo.VisualTargetItem as TreeViewItem;

                if (vSourceItem == vTargetItem || vSourceChild.ToList().Contains(vTargetItem) || vTargetItem == null)
                    dropInfo.Effects = DragDropEffects.None;
                else
                    dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as DataObject;

            if (dataObject != null && dataObject.ContainsFileDropList())
            {
                foreach (string str in dataObject.GetFileDropList())
                {
                    IAssets item = null;

                    if (File.Exists(str))
                        item = GetFileItem(str);

                    if(Directory.Exists(str))
                        item = GetItemFromDirectory(new DirectoryInfo(str));

                    if (item != null)
                    {
                        if (dropInfo.TargetItem is DirectoryItem)
                        {
                            var directoryItem = dropInfo.TargetItem as DirectoryItem;
                            directoryItem.Assets.Add(item);
                        }
                        else
                        {
                            ResourceItems[0].Assets.Add(item);
                        }
                    }
                }
                return;
            }
            else if (dropInfo.DragInfo.Data is IAssets)
            {
                if (dropInfo.TargetItem is DirectoryItem || dropInfo.TargetItem is RootItem)
                {
                    var sourceCollection = dropInfo.DragInfo.SourceCollection as ObservableCollection<IAssets>;
                    var targetDirectoryItem = dropInfo.TargetItem as IAssets;
                    var droppedAssets = dropInfo.Data as IAssets;
                    sourceCollection.Remove(droppedAssets);
                    targetDirectoryItem.Assets.Add(droppedAssets);
                    targetDirectoryItem.IsExpanded = true;
                    SelectedItem = droppedAssets;
                }
            }

            UpdateTextureItem(ResourceItems);
        }


        public void StartDrag(IDragInfo dragInfo)
        {
            if (dragInfo.SourceItem is IAssets )
            {
                IAssets item = (IAssets)dragInfo.SourceItem;
                dragInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                dragInfo.Data = item;
            }
        }

        public bool CanStartDrag(IDragInfo dragInfo)
        {
            if (dragInfo.SourceItem is IAssets && !(dragInfo.SourceItem is RootItem))
                return true;
            else
                return false;
        }

        public void Dropped(IDropInfo dropInfo)
        {
            //throw new NotImplementedException();
        }

        public void DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo)
        {
            //throw new NotImplementedException();
        }

        public void DragCancelled()
        {
            //throw new NotImplementedException();
        }

        public bool TryCatchOccurredException(Exception exception)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region COPY/PASTE
        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }

        public void SetViewModel(IModel model)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
