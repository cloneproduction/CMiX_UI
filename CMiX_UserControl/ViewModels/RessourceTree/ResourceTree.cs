using System;
using System.Collections.Generic;
using System.IO;
using CMiX.MVVM.ViewModels;

namespace CMiX.ViewModels
{
    public class ResourceTree : ViewModel
    {
        public ResourceTree()
        {
            ResourceItems = GetItems("C:\\Users\\BabyClone\\Google Drive");
        }

        private List<Item> _resourceItems;
        public List<Item> ResourceItems
        {
            get { return _resourceItems; }
            set { _resourceItems = value; }
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
    }
}
