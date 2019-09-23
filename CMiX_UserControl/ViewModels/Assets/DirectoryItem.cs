using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;

namespace CMiX.ViewModels
{
    public class DirectoryItem : Item
    {
        public DirectoryItem()
        {
            Items = new ObservableCollection<Item>();
            ParentDirectory = new ObservableCollection<Item>();
            IsEditable = false;
            IsSelected = false;

            PreviewMouseDownCommand = new RelayCommand(p => Edit(p));
            LostFocusCommand = new RelayCommand(p => LostFocus());
            
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value == false)
                    IsEditable = false;
                SetAndNotify(ref _isSelected, value);
            }
        }
        public void LostFocus()
        {
            IsEditable = false;
            Console.WriteLine("lost focus");
        }

        public void Edit(object p)
        {
            if (this.IsSelected)
                IsEditable = true;
        }

        public ICommand LostFocusCommand { get; set; }
        public ICommand PreviewMouseDownCommand { get; set; }

        private bool _isEditable;
        public bool IsEditable
        {
            get => _isEditable;
            set => SetAndNotify(ref _isEditable, value);
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
