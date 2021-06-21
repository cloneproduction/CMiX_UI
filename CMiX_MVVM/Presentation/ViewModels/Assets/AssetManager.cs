﻿using CMiX.Core.Presentation.Extensions;
using CMiX.Core.Presentation.ViewModels.Components;
using CMiX.Core.Tools;
using GongSolutions.Wpf.DragDrop;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CMiX.Core.Presentation.ViewModels.Assets
{
    public class AssetManager : ViewModel, IDropTarget, IDragSource
    {
        public AssetManager(Project project)
        {
            AssetFactory = new AssetFactory();

            DialogService = project.DialogService;
            Assets = project.Assets;

            this.Assets.Add(AssetFactory.CreateRootDirectory("RESOURCES"));

            AssetsFlatten = new ObservableCollection<Asset>();
            this.AssetsFlatten.CollectionChanged += FlattenAssets_CollectionChanged;

            SelectedItems = new ObservableCollection<Asset>();
            SelectedItems.CollectionChanged += CollectionChanged;

            InitCollectionView();

            AddAssetCommand = new RelayCommand(p => AddAsset());
            DeleteAssetsCommand = new RelayCommand(p => DeleteAssets());
            RenameAssetCommand = new RelayCommand(p => RenameAsset());
            RelinkAssetsCommand = new RelayCommand(p => RelinkAssets());
        }

        private AssetFactory AssetFactory { get; set; }

        private ObservableCollection<Asset> _assets;
        public ObservableCollection<Asset> Assets
        {
            get => _assets;
            set => SetAndNotify(ref _assets, value);
        }

        public void InitCollectionView()
        {
            GeometryViewSource = new CollectionViewSource();
            GeometryViewSource.Source = this.AssetsFlatten;
            GeometryCollectionView = GeometryViewSource.View;
            GeometryCollectionView.SortDescriptions.Add(new SortDescription(nameof(AssetGeometry.Name), ListSortDirection.Ascending));
            GeometryCollectionView.Filter = FilterGeometry;

            ImageViewSource = new CollectionViewSource();
            ImageViewSource.Source = this.AssetsFlatten;
            ImageCollectionView = ImageViewSource.View;
            ImageCollectionView.SortDescriptions.Add(new SortDescription(nameof(AssetTexture.Name), ListSortDirection.Ascending));
            ImageCollectionView.Filter = FilterImage;
        }

        private void FlattenAssets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            GeometryCollectionView.Refresh();
            ImageCollectionView.Refresh();
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

        private ObservableCollection<Asset> _assetsFlatten;
        public ObservableCollection<Asset> AssetsFlatten
        {
            get => _assetsFlatten;
            set => SetAndNotify(ref _assetsFlatten, value);
        }

        public void BuildAssetFlattenCollection(ObservableCollection<Asset> assets)
        {
            foreach (Asset asset in assets)
            {
                if (asset is AssetDirectory)
                    BuildAssetFlattenCollection(((AssetDirectory)asset).Assets);
                else
                    AssetsFlatten.Add(asset);
            }
        }


        public bool FilterGeometry(object item) => item is AssetGeometry ? true : false;
        public bool FilterImage(object item) => item is AssetTexture ? true : false;



        public void RenameAsset()
        {
            if (SelectedItems[0] is AssetDirectory)
                ((AssetDirectory)SelectedItems[0]).Rename();
        }

        public void RelinkAssets()
        {
            if (SelectedItems.Count > 0)
            {
                Asset asset = SelectedItems[0];
                OpenFileDialogSettings settings = new OpenFileDialogSettings();

                if (asset is AssetTexture)
                    settings.Filter = "Image |*.jpg;*.jpeg;*.png;*.dds";
                else if (asset is AssetGeometry)
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
            if (SelectedItems != null && SelectedItems.Count == 1 && SelectedItems[0] is AssetDirectory)
            {
                AssetDirectory selectedItem = (AssetDirectory)SelectedItems[0];
                selectedItem.IsExpanded = true;
                selectedItem.AddAsset(new AssetDirectory("New Folder"));
            }
        }

        private void DeleteAssets()
        {
            if (SelectedItems != null && this.Assets != null)
                DeleteSelectedAssets(this.Assets);
        }

        public void RemoveItemFromDirectory(AssetDirectory directory)
        {
            var toBeRemoved = new List<Asset>();
            foreach (var asset in directory.Assets)
            {
                if (asset is AssetDirectory)
                    RemoveItemFromDirectory(asset as AssetDirectory);

                toBeRemoved.Add(asset);
            }

            foreach (var item in toBeRemoved)
            {
                directory.Assets.Remove(item);
                this.Assets.Remove(item);
                this.AssetsFlatten.Remove(item);
            }
        }

        public void DeleteSelectedAssets(ObservableCollection<Asset> assets)
        {
            var toBeRemoved = new List<Asset>();

            foreach (var asset in assets)
            {
                if (asset.IsSelected)
                {
                    toBeRemoved.Add(asset);
                    if (asset is AssetDirectory)
                        RemoveItemFromDirectory(asset as AssetDirectory);
                }
                else if (!asset.IsSelected && asset is AssetDirectory)
                    DeleteSelectedAssets(((AssetDirectory)asset).Assets);
            }

            foreach (var item in toBeRemoved)
            {
                assets.Remove(item);
                this.Assets.Remove(item);
                this.AssetsFlatten.Remove(item);
            }

            SelectedItems.Clear();
        }



        private Asset GetItemFromDirectory(DirectoryInfo directoryInfo)
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



        private Asset GetFileItem(string filePath)
        {
            string fileType = Path.GetExtension(filePath).ToUpper().TrimStart('.');
            string fileName = Path.GetFileName(filePath);

            Asset item = null;

            foreach (var textureFileType in Enum.GetValues(typeof(TextureFileType)))
            {
                if (fileType == textureFileType.ToString())
                    item = this.AssetFactory.CreateAssetTexture(fileName, filePath);
            }

            foreach (var geometryFileType in Enum.GetValues(typeof(GeometryFileType)))
            {
                if (fileType == geometryFileType.ToString())
                    item = this.AssetFactory.CreateAssetGeometry(fileName, filePath);
            }

            if (item != null)
                this.AssetsFlatten.Add(item);

            return item;
        }

        public ICommand RenameAssetCommand { get; set; }
        public ICommand AddAssetCommand { get; set; }
        public ICommand DeleteAssetsCommand { get; set; }
        public ICommand DeleteSelectedItemCommand { get; set; }
        public ICommand RelinkAssetsCommand { get; set; }


        public IDialogService DialogService { get; set; }



        private ObservableCollection<Asset> _selectedItems;
        public ObservableCollection<Asset> SelectedItems
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


        public void DragOver(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as DataObject;

            if (dataObject != null && dataObject.ContainsFileDropList())
            {
                dropInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            }

            if (dropInfo.DragInfo != null && dropInfo.DragInfo.SourceItem is Asset)
            {
                var targetItem = dropInfo.TargetItem;
                var vSourceItem = dropInfo.DragInfo.VisualSourceItem as TreeViewItem;
                var vSourceChild = vSourceItem.FindVisualChildren<TreeViewItem>();// Utils.FindVisualChildren<TreeViewItem>(vSourceItem);
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
                    Asset item = null;

                    if (File.Exists(str))
                        item = GetFileItem(str);

                    if (Directory.Exists(str))
                        item = GetItemFromDirectory(new DirectoryInfo(str));

                    if (item != null)
                    {
                        if (dropInfo.TargetItem is AssetDirectory)
                        {
                            var directoryItem = (AssetDirectory)dropInfo.TargetItem;
                            directoryItem.Assets.Add(item);
                            directoryItem.SortAssets();
                            directoryItem.IsExpanded = true;
                        }
                        else if (this.Assets[0] is AssetDirectory)
                        {
                            this.AssetsFlatten.Add(item);
                            ((AssetDirectory)this.Assets[0]).Assets.Add(item);
                            ((AssetDirectory)this.Assets[0]).SortAssets();
                            ((AssetDirectory)this.Assets[0]).IsExpanded = true;
                        }
                    }
                }
            }
            else if (dropInfo.DragInfo.Data is List<AssetDragDrop> && dropInfo.TargetCollection is ObservableCollection<Asset>)
            {
                var targetCollection = dropInfo.TargetCollection as ObservableCollection<Asset>;
                if (targetCollection is ObservableCollection<Asset>)
                {
                    var targetItem = dropInfo.TargetItem;
                    if (targetItem is AssetDirectory)
                    {
                        var data = dropInfo.DragInfo.Data as List<AssetDragDrop>;
                        foreach (AssetDragDrop item in data)
                        {
                            item.DragObject.IsSelected = false;
                            targetCollection.Add(item.DragObject);
                            item.SourceCollection.Remove(item.DragObject);
                        }
                        ((AssetDirectory)targetItem).IsExpanded = true;
                        ((AssetDirectory)targetItem).SortAssets();
                    }
                }
            }
        }

        public void RemoveAssets(List<Asset> assetsToRemove, List<Asset> assets)
        {
            assets.RemoveAll(item => assetsToRemove.Contains(item));
        }

        public void GetDragDropObjects(List<AssetDragDrop> dragList, ObservableCollection<Asset> assets)
        {
            foreach (var asset in assets)
            {
                if (asset.IsSelected)
                {
                    AssetDragDrop dragDropObject = new AssetDragDrop();
                    dragDropObject.DragObject = asset;
                    dragDropObject.SourceCollection = assets;
                    dragList.Add(dragDropObject);
                }
                else if (!asset.IsSelected && asset is AssetDirectory)
                {
                    GetDragDropObjects(dragList, ((AssetDirectory)asset).Assets);
                }
            }
        }

        public void StartDrag(IDragInfo dragInfo)
        {
            List<AssetDragDrop> dragList = new List<AssetDragDrop>();
            GetDragDropObjects(dragList, this.Assets);

            if (dragList.Count > 0)
            {
                dragInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                dragInfo.Data = dragList;
            }
        }

        public bool CanStartDrag(IDragInfo dragInfo)
        {
            if (dragInfo.SourceItem is Asset)
                return true;
            else
                return false;
        }

        public void Dropped(IDropInfo dropInfo)
        {

        }

        public void DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo)
        {

        }

        public void DragCancelled()
        {

        }

        public bool TryCatchOccurredException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CanRenameAsset = (SelectedItems.Count == 1 && SelectedItems.OfType<AssetDirectory>().Any());
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
    }
}