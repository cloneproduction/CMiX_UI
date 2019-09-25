using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;
using GongSolutions.Wpf.DragDrop;

namespace CMiX.ViewModels
{
    public class Assets : ViewModel, IDropTarget, IDragSource
    {
        public Assets()
        {
            GeometryItems = new ObservableCollection<GeometryItem>();
            TextureItems = new ObservableCollection<TextureItem>();
            ResourceItems = new ObservableCollection<Item>();// GetItems("C:\\Users\\BabyClone\\Google Drive");

            AddNewFolderCommand = new RelayCommand(p => AddNewFolder());
        }


        public void AddNewFolder()
        {
            var item = new DirectoryItem
            {
                Name = "NewFolder",
                ParentDirectory = ResourceItems,
                //Path = directory.FullName,
                //Items = GetItems(directory.FullName)
            };
            ResourceItems.Add(item);
        }


        public ICommand AddNewFolderCommand { get; set; }


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

        public ObservableCollection<Item> GetItems(string path)
        {
            var items = new ObservableCollection<Item>();

            var dirInfo = new DirectoryInfo(path);

            foreach (var directory in dirInfo.GetDirectories())
            {
                var item = new DirectoryItem
                {
                    Name = directory.Name,
                    Path = directory.FullName,
                    Items = GetItems(directory.FullName),
                    ParentDirectory = items
                };
                items.Add(item);
            }

            foreach (var file in dirInfo.GetFiles())
            {
                var filetype = file.Extension.ToUpper();

                if(filetype == ".PNG" || filetype == ".JPG" || filetype == ".MOV")
                {
                    var texture = new TextureItem
                    {
                        Name = file.Name,
                        Path = file.FullName
                    };
                    if (!TextureItems.Contains(texture))
                        TextureItems.Add(texture);
                    items.Add(texture);
                }
                else if(filetype == ".FBX" || filetype == ".OBJ")
                {
                    var geometry = new GeometryItem
                    {
                        Name = file.Name,
                        Path = file.FullName
                    };
                    if(!GeometryItems.Contains(geometry))
                        GeometryItems.Add(geometry);
                    items.Add(geometry);
                }
            }
            return items;
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data.GetType() == typeof(DirectoryItem))
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Copy;
                return;
            }


            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            var dataObject = dropInfo.Data as IDataObject;
            var filedrop = dataObject.GetData(DataFormats.FileDrop, true);
            var filesOrDirectories = filedrop as String[];

            if (filesOrDirectories != null && filesOrDirectories.Length > 0)
            {
                foreach (string fullPath in filesOrDirectories)
                {
                    if (Directory.Exists(fullPath))
                    {
                        dropInfo.Effects = DragDropEffects.Copy;
                    }
                    else if (File.Exists(fullPath))
                    {
                        dropInfo.Effects = DragDropEffects.Copy;
                    }
                }
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if(dropInfo.Data is DirectoryItem)
            {
                var targetdirectoryitem = dropInfo.TargetItem as DirectoryItem;
                DirectoryItem droppedDirectoryItem = dropInfo.Data as DirectoryItem;

                if(targetdirectoryitem != droppedDirectoryItem)
                {
                    if (!targetdirectoryitem.Items.Contains(droppedDirectoryItem))
                    {
                        targetdirectoryitem.Items.Add(droppedDirectoryItem);
                        droppedDirectoryItem.ParentDirectory.Remove(droppedDirectoryItem);
                        droppedDirectoryItem.ParentDirectory = targetdirectoryitem.Items;
                        droppedDirectoryItem.IsSelected = false;
                    }
                }
            }

            var dataObject = dropInfo.Data as DataObject;
            if (dataObject != null)
            {
                if (dataObject.ContainsFileDropList())
                {
                    //Mementor.BeginBatch();
                    var filedrop = dataObject.GetFileDropList();

                    foreach (string str in filedrop)
                    {
                        if (Directory.Exists(str))
                        {
                            ResourceItems.Clear();
                            GeometryItems.Clear();
                            TextureItems.Clear();
                            this.ResourceItems = GetItems(str);
                        }
                        else if (File.Exists(str))
                        {
                        }
                    }
                    //Mementor.EndBatch();
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
    }
}
