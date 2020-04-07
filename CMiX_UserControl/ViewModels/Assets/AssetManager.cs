using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.OpenFile;

namespace CMiX.Studio.ViewModels
{
    public class AssetManager : ViewModel, IDropTarget, IDragSource, IGetSet<AssetsModel>
    {
        public AssetManager(IDialogService dialogService, ObservableCollection<IAssets> assets)
        {
            DialogService = dialogService;

            var directoryItem = new AssetDirectory("RESOURCES");
            directoryItem.IsRoot = true;

            Assets = assets;
            FlattenAssets = new ObservableCollection<IAssets>();

            Assets.Add(directoryItem);

            SelectedItems = new ObservableCollection<IAssets>();
            SelectedItems.CollectionChanged += CollectionChanged;

            AddAssetCommand = new RelayCommand(p => AddAsset());
            DeleteAssetsCommand = new RelayCommand(p => DeleteAssets());
            RenameAssetCommand = new RelayCommand(p => RenameAsset());
            RelinkAssetsCommand = new RelayCommand(p => RelinkAssets());

            InitCollectionView();
        }


        public void InitCollectionView()
        {

            this.FlattenAssets.CollectionChanged += FlattenAssets_CollectionChanged;

            GeometryViewSource = new CollectionViewSource();
            GeometryViewSource.Source = this.FlattenAssets;
            GeometryCollectionView = GeometryViewSource.View;
            GeometryCollectionView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            GeometryCollectionView.Filter = FilterGeometry;

            ImageViewSource = new CollectionViewSource();
            ImageViewSource.Source = this.FlattenAssets;
            ImageCollectionView = ImageViewSource.View;
            ImageCollectionView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            ImageCollectionView.Filter = FilterImage;
        }

        private void FlattenAssets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            GeometryCollectionView.Refresh();
            ImageCollectionView.Refresh();
        }

        public bool FilterGeometry(object item)
        {
            if (item is GeometryItem)
                return true;
            else
                return false;
        }

        public bool FilterImage(object item)
        {
            if (item is TextureItem)
                return true;
            else
                return false;
        }

        public CollectionViewSource GeometryViewSource { get; set; }

        private ICollectionView _geometryCollectionView;
        public ICollectionView GeometryCollectionView
        {
            get => _geometryCollectionView;
            set => SetAndNotify(ref _geometryCollectionView, value);
        }

        public CollectionViewSource ImageViewSource { get; set; }
        private ICollectionView _imageCollectionView;
        public ICollectionView ImageCollectionView
        {
            get => _imageCollectionView;
            set => SetAndNotify(ref _imageCollectionView, value);
        }

        #region METHODS
        public void RenameAsset()
        {
            if (SelectedItems[0] is IDirectory)
                ((IDirectory)SelectedItems[0]).Rename();
        }

        public void RelinkAssets()
        {
            if(SelectedItems.Count > 0)
            {
                IAssets asset = SelectedItems[0];
                OpenFileDialogSettings settings = new OpenFileDialogSettings();

                if (asset is TextureItem)
                    settings.Filter = "Image |*.jpg;*.jpeg;*.png;*.dds";
                else if (asset is GeometryItem)
                    settings.Filter = "Geometry |*.fbx; *.obj";

                bool? success = DialogService.ShowOpenFileDialog(this, settings);
                if (success == true)
                {
                    asset.Path = settings.FileName;
                    asset.Name = Path.GetFileName(settings.FileName);
                }
            }
        }

        public void AddAsset()
        {
            if (SelectedItems != null && SelectedItems.Count == 1 && SelectedItems[0] is IDirectory)
            {
                IDirectory selectedItem = (IDirectory)SelectedItems[0];
                selectedItem.IsExpanded = true;
                selectedItem.AddAsset(new AssetDirectory("New Folder"));
            }
        }

        private void DeleteAssets()
        {
            if (SelectedItems != null && Assets != null)
                DeleteSelectedAssets(Assets);
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
                Assets.Remove(item);
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
                Assets.Remove(item);
            }

            SelectedItems.Clear();
        }



        private IAssets GetItemFromDirectory(DirectoryInfo directoryInfo)
        {
            var directoryItem = new AssetDirectory(directoryInfo.Name);

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
                FlattenAssets.Add(item);

            return item;
        }
        #endregion

        #region PROPERTIES
        public ICommand RenameAssetCommand { get; set; }
        public ICommand AddAssetCommand { get; set; }
        public ICommand DeleteAssetsCommand { get; set; }
        public ICommand DeleteSelectedItemCommand { get; set; }
        public ICommand RelinkAssetsCommand { get; set; }

        private ObservableCollection<IAssets> _assets;
        public ObservableCollection<IAssets> Assets
        {
            get => _assets;
            set => SetAndNotify(ref _assets, value);
        }

        private ObservableCollection<IAssets> _flattenAssets;
        public ObservableCollection<IAssets> FlattenAssets
        {
            get => _flattenAssets;
            set => SetAndNotify(ref _flattenAssets, value);
        }

        public IDialogService DialogService { get; set; }

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

        private bool _canRelinkAsset = false;
        public bool CanRelinkAsset
        {
            get => _canRelinkAsset;
            set => SetAndNotify(ref _canRelinkAsset, value);
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

            if (dropInfo.DragInfo != null && dropInfo.DragInfo.SourceItem is IAssets)
            {
                var targetItem = dropInfo.TargetItem;
                var vSourceItem = dropInfo.DragInfo.VisualSourceItem as TreeViewItem;
                var vSourceChild = Utils.FindVisualChildren<TreeViewItem>(vSourceItem);
                var vTargetItem = dropInfo.VisualTargetItem as TreeViewItem;

                if (targetItem is AssetDirectory)
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
                        else if (Assets[0] is IDirectory)
                        {
                            FlattenAssets.Add(item);
                            ((IDirectory)Assets[0]).Assets.Add(item);
                            ((IDirectory)Assets[0]).SortAssets();
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
            GetDragDropObjects(dragList, Assets);
            
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
            CanRenameAsset = (SelectedItems.Count == 1 && SelectedItems.OfType<AssetDirectory>().Any() );
            CanAddAsset = (SelectedItems.Count == 1 && SelectedItems.OfType<AssetDirectory>().Any());
            CanDeleteAsset = !SelectedItems.OfType<AssetDirectory>().Any(c => c.IsRoot == true);
            CanRelinkAsset = (SelectedItems.Count == 1 && !SelectedItems.OfType<AssetDirectory>().Any());

            if (SelectedItems.Count == 0)
            {
                CanAddAsset = false;
                CanRenameAsset = false;
                CanDeleteAsset = false;
                CanRelinkAsset = false;
            }
        }

        #region GETSETMODEL
        public AssetsModel GetModel()
        {
            AssetsModel assetsModel = new AssetsModel();
            foreach (var asset in Assets)
            {
                assetsModel.AssetModels.Add(asset.GetModel());
            }
            return assetsModel;
        }

        public void SetViewModel(AssetsModel model)
        {
            Assets.Clear();
            foreach (var assetModel in model.AssetModels)
            {
                IAssets asset = null;

                if(assetModel is DirectoryAssetModel)
                {
                    Console.WriteLine("assetModel is DirectoryAssetModel");
                    asset = new AssetDirectory();
                    Console.WriteLine("Directory Asset Count " + assetModel.AssetModels.Count);
                }
                    
                else if (assetModel is GeometryAssetModel)
                {
                    Console.WriteLine("assetModel is GeometryAssetModel");
                    asset = new GeometryItem();
                }
                    
                else if (assetModel is TextureAssetModel)
                {
                    Console.WriteLine("assetModel is TextureAssetModel");
                    asset = new TextureItem();
                }

                asset.SetViewModel(assetModel);
                Assets.Add(asset);
            }
        }
        #endregion
    }
}
