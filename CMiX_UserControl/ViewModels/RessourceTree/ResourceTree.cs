using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using CMiX.MVVM.ViewModels;
using GongSolutions.Wpf.DragDrop;
namespace CMiX.ViewModels
{
    public class ResourceTree : ViewModel, IDropTarget
    {
        public ResourceTree()
        {
            ResourceItems = GetItems("C:\\Users\\BabyClone\\Google Drive");
        }

        private List<Item> _resourceItems;
        public List<Item> ResourceItems
        {
            get => _resourceItems;
            set => SetAndNotify(ref _resourceItems, value);
        }

        public List<Item> GetItems(string path)
        {
            var items = new List<Item>();

            var dirInfo = new DirectoryInfo(path);

            foreach (var directory in dirInfo.GetDirectories())
            {
                var item = new DirectoryItem
                {
                    Name = directory.Name,
                    Path = directory.FullName,
                    Items = GetItems(directory.FullName)
                };
                items.Add(item);
            }

            foreach (var file in dirInfo.GetFiles())
            {
                var item = new FileItem
                {
                    Name = file.Name,
                    Path = file.FullName
                };

                items.Add(item);
            }

            return items;
        }

        public void DragOver(IDropInfo dropInfo)
        {
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
                            Console.WriteLine(str);
                            this.ResourceItems = GetItems(str);
                            Console.WriteLine(ResourceItems[0].Name);
                        }
                        else if (File.Exists(str))
                        {
                            Console.WriteLine("File Pouet");
                        }
                    }
                    //Mementor.EndBatch();
                }
            }

        }
    }
}
