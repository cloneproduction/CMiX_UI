using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public class Item : ViewModel
    {
        public Item()
        {
            RenameCommand = new RelayCommand(p => Rename());
            RemoveItemCommand = new RelayCommand(p => RemoveItem(p as Item));
            AddDirectoryItemCommand = new RelayCommand(p => AddDirectoryItem());
            Items = new ObservableCollection<Item>();
        }

        private void Rename()
        {
            IsRenaming = true;
        }

        public ICommand RenameCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }
        public ICommand AddDirectoryItemCommand { get; set; }

        private string _path;
        public string Path
        {
            get { return _path; }
            set => SetAndNotify(ref _path, value);
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set => SetAndNotify(ref _name, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndNotify(ref _isSelected, value);
        }

        private bool _isRenaming;
        public bool IsRenaming
        {
            get => _isRenaming;
            set => SetAndNotify(ref _isRenaming, value);
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetAndNotify(ref _isExpanded, value);
        }

        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public void RemoveItem(Item item)
        {
            if (Items.Contains(item))
                Items.Remove(item);
        }

        public void AddDirectoryItem()
        {
            DirectoryItem directoryItem = new DirectoryItem("New Folder", null);
            Items.Add(directoryItem);
        }
    }
}
