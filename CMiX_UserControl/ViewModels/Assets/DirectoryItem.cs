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
            ParentDirectory = new ObservableCollection<Item>();
            IsExpanded = false;
            Name = name;
            Path = path;
            DoubleClickCommand = new RelayCommand(p => DoubleClick(p));
            SingleClickCommand = new RelayCommand(p => SingleClick(p));
            PreviewMouseUpCommand = new RelayCommand(p => PreviewMouseUp());
        }

        public ICommand DoubleClickCommand { get; set; }
        public ICommand SingleClickCommand { get; set; }
        public ICommand PreviewMouseUpCommand { get; set; }

        private void PreviewMouseUp()
        {

        }

        private void DoubleClick(object obj)
        {

        }

        private void SingleClick(object obj)
        {

        }

        private bool _canEdit;
        public bool CanEdit
        {
            get => _canEdit;
            set
            {
                SetAndNotify(ref _canEdit, value);
            }
        }

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                SetAndNotify(ref _isEditing, value);
            }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                SetAndNotify(ref _isExpanded, value);
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
