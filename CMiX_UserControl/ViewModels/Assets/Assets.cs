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
using System.Linq.Dynamic;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.Specialized;

namespace CMiX.Studio.ViewModels
{
    public class Assets : ViewModel, IDropTarget, IDragSource
    {
        public Assets()
        {
            GeometryItems = new ObservableCollection<GeometryItem>();
            TextureItems = new ObservableCollection<TextureItem>();
            ResourceItems = new ObservableCollection<IAssets>();
            
            
            SelectedItems = new ObservableCollection<IAssets>();
            SelectedItems.CollectionChanged += CollectionChanged;
            var directoryItem = new RootItem();
            ResourceItems.Add(directoryItem);
            //SelectedItem = directoryItem;
            InitializeCollectionView();

            AddAssetCommand = new RelayCommand(p => AddAsset());
            DeleteAssetsCommand = new RelayCommand(p => DeleteAssets());
            RenameAssetCommand = new RelayCommand(p => RenameAsset());
        }

        #region METHODS

        private ObservableCollection<IAssets> _selectedItems;
        public ObservableCollection<IAssets> SelectedItems
        {
            get => _selectedItems;
            set => SetAndNotify(ref _selectedItems, value);
        }

        private bool _canAddAsset;
        public bool CanAddAsset
        {
            get => _canAddAsset;
            set => SetAndNotify(ref _canAddAsset, value);
        }

        private bool _canRenameAsset;
        public bool CanRenameAsset
        {
            get => _canRenameAsset;
            set => SetAndNotify(ref _canRenameAsset, value);
        }

        public void RenameAsset()
        {
            SelectedItems[0].IsRenaming = true;
        }

        private void DeleteAssets()
        {

            if (SelectedItems != null && ResourceItems != null)
                DeleteItems(ResourceItems);
        }

        public void DeleteItems(ObservableCollection<IAssets> assets)
        {
            var toBeRemoved = new List<IAssets>();

            foreach (var asset in assets)
            {
                if (asset.IsSelected)
                    toBeRemoved.Add(asset);
                else
                    DeleteItems(asset.Assets);
            }

            foreach (var item in toBeRemoved)
                assets.Remove(item);

            SelectedItems.Clear();
        }

        public void AddAsset()
        {
            var directoryItem = new DirectoryItem("NewFolder", null, null);
            SelectedItems[0].Assets.Add(directoryItem);
        }


        #endregion

        #region PROPERTIES
        public ICommand RenameAssetCommand { get; set; }
        public ICommand AddAssetCommand { get; set; }
        public ICommand AddNewFolderCommand { get; set; }
        public ICommand DeleteAssetsCommand { get; set; }
        public ICommand DeleteSelectedItemCommand { get; set; }

        private ObservableCollection<IAssets> _resourceItems;
        public ObservableCollection<IAssets> ResourceItems
        {
            get => _resourceItems;
            set => SetAndNotify(ref _resourceItems, value);
        }

        public ObservableCollection<GeometryItem> GeometryItems;
        public ObservableCollection<TextureItem> TextureItems;

        private void UpdateTextureItem(ObservableCollection<IAssets> assets)
        {
            foreach (var asset in assets)
            {
                if (asset is TextureItem && !assets.Contains(asset))
                    TextureItems.Add(asset as TextureItem);
                else if (asset is DirectoryItem)
                    UpdateTextureItem(((DirectoryItem)asset).Assets);
            }
        }

        private IAssets GetItemFromDirectory(DirectoryInfo directoryInfo)
        {
            var directoryItem = new DirectoryItem(directoryInfo.Name, directoryInfo.FullName, null);

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

        public CollectionViewSource  AssetsCollectionView { get; set; }

        private void InitializeCollectionView()
        {
            AssetsCollectionView = new CollectionViewSource();
            AssetsCollectionView.Source = ResourceItems;
            SortDescription ponderation = new SortDescription("Ponderation", ListSortDirection.Ascending);
            SortDescription sort = new SortDescription("Name", ListSortDirection.Ascending);
            AssetsCollectionView.SortDescriptions.Add(ponderation);
            AssetsCollectionView.SortDescriptions.Add(sort);
        }

        #region DRAG DROP
        public void DragOver(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as DataObject;


            if (dataObject != null && dataObject.ContainsFileDropList())
                dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;

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
                        if (dropInfo.TargetItem is DirectoryItem)
                        {
                            var directoryItem = (DirectoryItem)dropInfo.TargetItem;
                            directoryItem.Assets.Add(item);
                        }
                        else
                            ResourceItems[0].Assets.Add(item);
                    }
                }
            }
            else if (dropInfo.DragInfo.Data is List<DragDropObject> && dropInfo.TargetCollection is ListCollectionView)
            {
                var targetViewCollection = (dropInfo.TargetCollection as ListCollectionView).SourceCollection;
                if (targetViewCollection is ObservableCollection<IAssets>)
                { 
                    var targetItem = dropInfo.TargetItem;
                    if (targetItem is DirectoryItem || targetItem is RootItem)
                    {
                        var targetCollection = targetViewCollection as ObservableCollection<IAssets>;
                        if (targetCollection != null)
                        {
                            var data = dropInfo.DragInfo.Data as List<DragDropObject>;
                            foreach (DragDropObject item in data)
                            {
                                targetCollection.Add(item.DragObject);
                                item.SourceCollection.Remove(item.DragObject);
                            }
                        }
                    }
                }
            }
            UpdateTextureItem(ResourceItems);
        }

        public void RemoveAssets(List<IAssets> assetsToRemove, List<IAssets> assets)
        {
            assets.RemoveAll(item => assetsToRemove.Contains(item));
        }

        //var movies = _db.Movies.Where(p => p.Genres.Intersect(listOfGenres).Any());

        public void GetDragDropObjects(List<DragDropObject> dragList, ObservableCollection<IAssets> assets)
        {
            foreach (var asset in assets)
            {
                if (asset.IsSelected)
                {
                    DragDropObject dragDropObject = new DragDropObject();
                    dragDropObject.DragObject = asset;
                    dragDropObject.SourceCollection = assets;
                    dragList.Add(dragDropObject);
                }
                else
                {
                    GetDragDropObjects(dragList, asset.Assets);
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
            if (dragInfo.SourceItem is IAssets && !(dragInfo.SourceItem is RootItem))
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
            Console.WriteLine("DragCancelled");
            //throw new NotImplementedException();
        }

        public bool TryCatchOccurredException(Exception exception)
        {
            throw new NotImplementedException();
        }
        #endregion

        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("SelectedItems Count " + SelectedItems.Count);
            if(SelectedItems.Count > 0 )
            {
                if(SelectedItems[0] is RootItem || SelectedItems[0] is DirectoryItem)
                {
                    CanAddAsset = true;
                    CanRenameAsset = true;
                }
            }
            else
            {
                CanAddAsset = false;
                CanRenameAsset = false;
            }
                
            //if(SelectedItems != null)
            //{
            //    foreach (var item in SelectedItems)
            //    {
            //        if(item != null)
            //            Console.WriteLine(item.Name);
            //    }
            //}


                //if (e.OldItems != null)
                //{
                //    foreach (INotifyPropertyChanged item in e.OldItems)
                //        item.PropertyChanged -= item_PropertyChanged;
                //}
                //if (e.NewItems != null)
                //{
                //    foreach (INotifyPropertyChanged item in e.NewItems)
                //        item.PropertyChanged += item_PropertyChanged;
                //}
        }

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
