using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using GongSolutions.Wpf.DragDrop;

namespace CMiX.Studio.ViewModels
{
    public class Assets : ViewModel, IDropTarget, IDragSource
    {
        public Assets()
        {
            GeometryItems = new ObservableCollection<GeometryItem>();
            TextureItems = new ObservableCollection<TextureItem>();
            ResourceItems = new ObservableCollection<Item>();

            AddNewFolderCommand = new RelayCommand(p => AddNewFolder());
            RenameFolderCommand = new RelayCommand(p => RenameFolder());
            DeleteItemCommand = new RelayCommand(p => DeleteItem());
        }

        #region METHODS
        private void DeleteItem()
        {
            if (SelectedItem != null)
            {
                ResourceItems.Remove(SelectedItem);
                UpdateTextureItem(ResourceItems);
            }
                
        }

        public void AddNewFolder()
        {
            var item = new DirectoryItem("NewFolder", null)
            {
                ParentDirectory = ResourceItems,
                //Path = directory.FullName,
                //Items = GetItems(directory.FullName)
            };

            if (SelectedItem != null)
            {
                ResourceItems.Insert(ResourceItems.IndexOf(SelectedItem), item);
            }
            else
                ResourceItems.Add(item);
        }

        private void RenameFolder()
        {
            if(SelectedItem is DirectoryItem)
            {
                DirectoryItem dirItem = SelectedItem as DirectoryItem;
                dirItem.IsEditing = true;
            }
        }
        #endregion

        #region PROPERTIES
        public ICommand RenameFolderCommand { get; set; }
        public ICommand AddNewFolderCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }

        private Item _selectedItem;
        public Item SelectedItem
        {
            get => _selectedItem;
            set => SetAndNotify(ref _selectedItem, value);
        }

        private ObservableCollection<Item> _resourceItems;
        public ObservableCollection<Item> ResourceItems
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

        private void UpdateTextureItem(ObservableCollection<Item> items)
        {
            foreach (var item in items)
            {
                if (item is TextureItem)
                    TextureItems.Add(item as TextureItem);
                else if (item is DirectoryItem)
                {
                    var directoryItems = item as DirectoryItem;
                    UpdateTextureItem(directoryItems.Items);
                }
            }
        }

        private Item GetItemFromDirectory(DirectoryInfo directoryInfo)
        {
            var directoryItem = new DirectoryItem(directoryInfo.Name, directoryInfo.FullName);
            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryItem.Items.Add(GetItemFromDirectory(directory));
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                var filetype = file.Extension.ToUpper().TrimStart('.');
                if (filetype == TextureFileType.PNG.ToString() || filetype == TextureFileType.JPG.ToString() || filetype == TextureFileType.MOV.ToString())
                {
                    var texture = new TextureItem(file.Name, file.FullName);
                    directoryItem.Items.Add(texture);
                }
                else if (filetype == GeometryFileType.FBX.ToString() || filetype == GeometryFileType.OBJ.ToString())
                {
                    var geometry = new GeometryItem(file.Name, file.FullName);
                    directoryItem.Items.Add(geometry);
                }
            }
            return directoryItem;
        }
        #endregion

        #region DRAG DROP
        public void DragOver(IDropInfo dropInfo)
        {
            if(dropInfo.Data.GetType() != typeof(Item))
                dropInfo.Effects = DragDropEffects.Copy;
        }

        public void Drop(IDropInfo dropInfo)
        {
            if(dropInfo.Data is Item && dropInfo.TargetItem is DirectoryItem)
            {
                var droppedDirectoryItem = dropInfo.Data as Item;
                var targetDirectoryItem = dropInfo.TargetItem as DirectoryItem;
                var sourceCollection = dropInfo.DragInfo.SourceCollection as ObservableCollection<Item>;

                sourceCollection.Remove(droppedDirectoryItem);
                targetDirectoryItem.Items.Add(droppedDirectoryItem);;
            }

            var dataObject = dropInfo.Data as DataObject;

            if (dataObject != null)
            {
                if (dataObject.ContainsFileDropList())
                {
                    var fileDrop = dataObject.GetFileDropList();
                    foreach (string str in fileDrop)
                    {
                        var dirInfo = new DirectoryInfo(str);
                        ResourceItems.Add(GetItemFromDirectory(dirInfo));
                    }
                    UpdateTextureItem(ResourceItems);
                }
            }
        }

        public void StartDrag(IDragInfo dragInfo)
        {
            if (dragInfo.SourceItem is DirectoryItem)
            {
                DirectoryItem directoryitem = (DirectoryItem)dragInfo.SourceItem;
                dragInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                dragInfo.Data = directoryitem;
                return;
            }
            else if(dragInfo.SourceItem is Item)
            {
                Item item = (Item)dragInfo.SourceItem;
                dragInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                dragInfo.Data = item;
            }
        }

        public bool CanStartDrag(IDragInfo dragInfo)
        {
            if (dragInfo.SourceItem is Item)
                return true;
            return false;
        }

        public void Dropped(IDropInfo dropInfo)
        {
            if (dropInfo.DragInfo != null)
            {
                if (dropInfo.DragInfo.VisualSource == dropInfo.VisualTarget && dropInfo.Data is Item)
                {
                    Item item = dropInfo.Data as Item;
                    if(dropInfo.VisualTarget.GetType() == typeof(DirectoryItem))
                    {
                        Console.WriteLine("DropOnDirectoryItem");
                    }
                    //FilePaths.Insert(dropInfo.InsertIndex, newfilenameitem);
                    //FilePaths.Remove(filenameitem);
                    //newfilenameitem.FileIsSelected = true;
                    //SelectedFileNameItem = newfilenameitem;
                    //Mementor.ElementAdd(FilePaths, newfilenameitem);
                }
            }
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
