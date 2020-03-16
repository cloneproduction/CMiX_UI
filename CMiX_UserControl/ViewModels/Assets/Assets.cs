using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.ViewModels;
using GongSolutions.Wpf.DragDrop;

namespace CMiX.Studio.ViewModels
{
    public class Assets : ViewModel, IDropTarget, IDragSource, IGetSet<AssetsModel>
    {
        public Assets()
        {
            AssetsViewSource = new CollectionViewSource();

            ResourceItems = new ObservableCollection<IAssets>();
            var directoryItem = new DirectoryItem("RESOURCES");
            directoryItem.IsRoot = true;
            ResourceItems.Add(directoryItem);

            AvailableResources = new ObservableCollection<IAssets>();
            AvailableResources.CollectionChanged += AvailableResources_CollectionChanged;
            AssetsViewSource.Source = AvailableResources;

            SelectedItems = new ObservableCollection<IAssets>();
            SelectedItems.CollectionChanged += CollectionChanged;

            AddAssetCommand = new RelayCommand(p => AddAsset());
            DeleteAssetsCommand = new RelayCommand(p => DeleteAssets());
            RenameAssetCommand = new RelayCommand(p => RenameAsset());
        }

        private void AvailableResources_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            AssetsViewSource.DeferRefresh();
        }

        #region METHODS
        public CollectionViewSource AssetsViewSource { get; set; }

        private ObservableCollection<IAssets> _selectedItems;
        public ObservableCollection<IAssets> SelectedItems
        {
            get => _selectedItems;
            set => SetAndNotify(ref _selectedItems, value);
        }

        private bool _canAddAsset = false;
        public bool CanAddAsset
        {
            get => _canAddAsset;
            set => SetAndNotify(ref _canAddAsset, value);
        }

        private bool _canRenameAsset = false;
        public bool CanRenameAsset
        {
            get => _canRenameAsset;
            set => SetAndNotify(ref _canRenameAsset, value);
        }

        private bool _canDeleteAsset = false;
        public bool CanDeleteAsset
        {
            get => _canDeleteAsset;
            set => SetAndNotify(ref _canDeleteAsset, value);
        }

        public void RenameAsset()
        {
            if(SelectedItems[0] is IDirectory)
                ((IDirectory)SelectedItems[0]).Rename();
        }

        private void DeleteAssets()
        {
            if (SelectedItems != null && ResourceItems != null)
                DeleteSelectedAssets(ResourceItems);
        }

        public void RemoveItemFromDirectory(IDirectory directory)
        {
            var toBeRemoved = new List<IAssets>();
            foreach (var asset in directory.Assets)
            {
                if(asset is IDirectory)
                    RemoveItemFromDirectory(asset as IDirectory);
                toBeRemoved.Add(asset);
            }
            
            foreach (var item in toBeRemoved)
            {
                directory.Assets.Remove(item);
                AvailableResources.Remove(item);
            }
        }

        public void DeleteSelectedAssets(ObservableCollection<IAssets> assets)
        {
            var toBeRemoved = new List<IAssets>();

            foreach (var asset in assets)
            {
                if (asset.IsSelected)
                {
                    toBeRemoved.Add(asset);
                    if (asset is IDirectory)
                        RemoveItemFromDirectory(asset as IDirectory);
                }
                else if (!asset.IsSelected && asset is IDirectory)
                    DeleteSelectedAssets(((IDirectory)asset).Assets);
            }

            foreach (var item in toBeRemoved)
            {
                assets.Remove(item);
                AvailableResources.Remove(item);
            }

            SelectedItems.Clear();
        }

        public void AddAsset()
        {
            if(SelectedItems != null && SelectedItems.Count == 1 && SelectedItems[0] is IDirectory)
            {
                IDirectory selectedItem = (IDirectory)SelectedItems[0];
                selectedItem.IsExpanded = true;
                selectedItem.AddAsset(new DirectoryItem("New Folder"));
            } 
        }
        #endregion

        #region PROPERTIES
        public ICommand RenameAssetCommand { get; set; }
        public ICommand AddAssetCommand { get; set; }
        public ICommand DeleteAssetsCommand { get; set; }
        public ICommand DeleteSelectedItemCommand { get; set; }

        private ObservableCollection<IAssets> _resourceItems;
        public ObservableCollection<IAssets> ResourceItems
        {
            get => _resourceItems;
            set => SetAndNotify(ref _resourceItems, value);
        }

        private ObservableCollection<IAssets> _availableResources;
        public ObservableCollection<IAssets> AvailableResources
        {
            get => _availableResources;
            set => SetAndNotify(ref _availableResources, value);
        }

        private IAssets GetItemFromDirectory(DirectoryInfo directoryInfo)
        {
            var directoryItem = new DirectoryItem(directoryInfo.Name);

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
            IAssets item = null;

            if (fileType == TextureFileType.PNG.ToString() || fileType == TextureFileType.JPG.ToString() || fileType == TextureFileType.MOV.ToString())
                item = new TextureItem(fileName, filePath);
            else if (fileType == GeometryFileType.FBX.ToString() || fileType == GeometryFileType.OBJ.ToString())
                item = new GeometryItem(fileName, filePath);

            if (item != null)
                AvailableResources.Add(item);

            return item;
        }
        #endregion

        #region DRAG DROP
        public void DragOver(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as DataObject;

            if (dataObject != null && dataObject.ContainsFileDropList())
            {
                dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            }
                

            if (dropInfo.DragInfo != null)
            {
                var targetItem = dropInfo.TargetItem;
                var vSourceItem = dropInfo.DragInfo.VisualSourceItem as TreeViewItem;
                var vSourceChild = Utils.FindVisualChildren<TreeViewItem>(vSourceItem);
                var vTargetItem = dropInfo.VisualTargetItem as TreeViewItem;

                if (targetItem is DirectoryItem)
                    dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;

                if (vSourceItem == vTargetItem || vSourceChild.ToList().Contains(vTargetItem) || vTargetItem == null)
                    dropInfo.Effects = DragDropEffects.None;
                else
                    dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
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

                    if (Directory.Exists(str))
                        item = GetItemFromDirectory(new DirectoryInfo(str));

                    if (item != null)
                    {
                        if (dropInfo.TargetItem is IDirectory)
                        {
                            var directoryItem = (IDirectory)dropInfo.TargetItem;
                            directoryItem.Assets.Add(item);
                            directoryItem.SortAssets();
                            directoryItem.IsExpanded = true;
                        }
                        else if(ResourceItems[0] is IDirectory)
                        {
                            ((IDirectory)ResourceItems[0]).Assets.Add(item);
                            ((IDirectory)ResourceItems[0]).SortAssets();
                        }
                    }
                }
            }
            else if (dropInfo.DragInfo.Data is List<DragDropObject> && dropInfo.TargetCollection is ObservableCollection<IAssets>)
            {
                var targetCollection = dropInfo.TargetCollection as ObservableCollection<IAssets>;
                if (targetCollection is ObservableCollection<IAssets>)
                {
                    var targetItem = dropInfo.TargetItem;
                    if (targetItem is IDirectory)
                    {
                        var data = dropInfo.DragInfo.Data as List<DragDropObject>;
                        foreach (DragDropObject item in data)
                        {
                            item.DragObject.IsSelected = false;
                            targetCollection.Add(item.DragObject);
                            item.SourceCollection.Remove(item.DragObject);
                        }
                        ((IDirectory)targetItem).IsExpanded = true;
                        ((IDirectory)targetItem).SortAssets();
                    }
                }
            }
        }

        public void RemoveAssets(List<IAssets> assetsToRemove, List<IAssets> assets)
        {
            assets.RemoveAll(item => assetsToRemove.Contains(item));
        }

        public void GetDragDropObjects(List<DragDropObject> dragList, ObservableCollection<IAssets> assets)
        {
            foreach (var asset in assets)
            {
                if (asset.IsSelected)
                {
                    Console.WriteLine("asset.IsSelected" + asset.Name);
                    DragDropObject dragDropObject = new DragDropObject();
                    dragDropObject.DragObject = asset;
                    dragDropObject.SourceCollection = assets;
                    dragList.Add(dragDropObject);
                }
                else if (!asset.IsSelected && asset is IDirectory)
                {
                    GetDragDropObjects(dragList, ((IDirectory)asset).Assets);
                }
            }
        }

        public void StartDrag(IDragInfo dragInfo)
        {
            List<DragDropObject> dragList = new List<DragDropObject>();
            GetDragDropObjects(dragList, ResourceItems);
            
            if(dragList.Count > 0)
            {
                dragInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                dragInfo.Data = dragList;
            }
        }

        public bool CanStartDrag(IDragInfo dragInfo)
        {
            if (dragInfo.SourceItem is IAssets)
                return true;
            else
                return false;
        }

        public void Dropped(IDropInfo dropInfo)
        {
            //Console.WriteLine("Dropped");
            //throw new NotImplementedException();
        }

        public void DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo)
        {
            //Console.WriteLine("DragDropOperationFinished");
            //throw new NotImplementedException();
        }

        public void DragCancelled()
        {

        }

        public bool TryCatchOccurredException(Exception exception)
        {
            throw new NotImplementedException();
        }
        #endregion

        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CanRenameAsset = (SelectedItems.Count == 1 && SelectedItems.OfType<DirectoryItem>().Any() && !SelectedItems.OfType<DirectoryItem>().Any(c => c.IsRoot == true));
            CanAddAsset = (SelectedItems.Count == 1 && SelectedItems.OfType<DirectoryItem>().Any());
            CanDeleteAsset = !SelectedItems.OfType<DirectoryItem>().Any(c => c.IsRoot == true);

            if (SelectedItems.Count == 0)
            {
                CanAddAsset = false;
                CanRenameAsset = false;
                CanDeleteAsset = false;
            }
        }


        #region COPY/PASTE
        public void CopyModel(IComponentModel model)
        {
            throw new NotImplementedException();
        }

        public void SetViewModel(IComponentModel model)
        {
            throw new NotImplementedException();
        }

        public AssetsModel GetModel()
        {
            AssetsModel assetsModel = new AssetsModel();


            
            return assetsModel;
        }

        public void SetViewModel(AssetsModel model)
        {
            
        }
        #endregion
    }
}
