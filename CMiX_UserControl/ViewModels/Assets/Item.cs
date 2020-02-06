using System;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public class Item : ViewModel
    {
        public Item()
        {
            RenameCommand = new RelayCommand(p => Rename());
        }

        private void Rename()
        {
            IsRenaming = true;
        }

        public ICommand RenameCommand { get; set; }

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
    }
}
