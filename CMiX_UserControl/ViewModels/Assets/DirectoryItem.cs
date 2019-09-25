using System;
using System.Collections.ObjectModel;

namespace CMiX.ViewModels
{
    public class DirectoryItem : Item
    {
        public DirectoryItem()
        {
            Items = new ObservableCollection<Item>();
            ParentDirectory = new ObservableCollection<Item>();    
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                SetAndNotify(ref _isSelected, value);
            }
        }

        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        private ObservableCollection<Item> _parentDirectory;
        public ObservableCollection<Item> ParentDirectory
        {
            get { return _parentDirectory; }
            set { _parentDirectory = value; }
        }

    }
}
