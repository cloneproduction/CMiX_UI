using CMiX.MVVM.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class DirectoryItem : Item
    {
        public DirectoryItem()
        {
            Items = new ObservableCollection<Item>();
            ParentDirectory = new ObservableCollection<Item>();
            IsExpanded = false;

            DoubleClickCommand = new RelayCommand(p => DoubleClick(p));
            SingleClickCommand = new RelayCommand(p => SingleClick(p));
            PreviewMouseUpCommand = new RelayCommand(p => PreviewMouseUp());
        }

        public ICommand DoubleClickCommand { get; set; }
        public ICommand SingleClickCommand { get; set; }
        public ICommand PreviewMouseUpCommand { get; set; }

        private void PreviewMouseUp()
        {
            if (CanEdit)
                IsEditing = true;
            //Console.WriteLine("Preview MouseUp"); //handle the double click event here...
        }

        private void DoubleClick(object obj)
        {
            //Console.WriteLine("Double Click!"); //handle the double click event here...
            DirectoryItem DItem = obj as DirectoryItem;
        }

        private void SingleClick(object obj)
        {
            //Console.WriteLine("Single Click!"); //handle the double click event here...
            DirectoryItem DItem = obj as DirectoryItem;
            DItem.CanEdit = true;
        }

        private bool _canEdit;
        public bool CanEdit
        {
            get => _canEdit;
            set
            {
                SetAndNotify(ref _canEdit, value);
                Console.WriteLine("CanEdit " + CanEdit.ToString());
            }
        }

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                SetAndNotify(ref _isEditing, value);
                Console.WriteLine("IsEditing " + IsEditing.ToString());
            }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                SetAndNotify(ref _isExpanded, value);
                Console.WriteLine("IsExpanded " + IsExpanded.ToString());
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                SetAndNotify(ref _isSelected, value);
                Console.WriteLine("IsSelected " + IsSelected.ToString());
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
