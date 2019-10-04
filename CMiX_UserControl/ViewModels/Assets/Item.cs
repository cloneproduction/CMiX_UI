using System;
using CMiX.MVVM.ViewModels;

namespace CMiX.ViewModels
{
    public class Item : ViewModel
    {
        public Item()
        {
        }

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
    }
}
