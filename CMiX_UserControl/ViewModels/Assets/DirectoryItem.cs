using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class DirectoryItem : Item
    {
        public DirectoryItem(string name, string path)
        {
            Items = new ObservableCollection<Item>();
            IsExpanded = false;
            Name = name;
            Path = path;

            AddNewDirectoryItemCommand = new RelayCommand(p => AddNewDirectoryItem());
        }

        public ICommand AddNewDirectoryItemCommand { get; set; }


        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public void AddNewDirectoryItem()
        {
            DirectoryItem directoryItem = new DirectoryItem("NewFolder", null);
            Items.Add(directoryItem);
        }
    }
}
