using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System.IO;

namespace CMiX.MVVM.ViewModels
{
    public class Asset : ViewModel, IAssets
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private string _ponderation = "aa";
        public string Ponderation
        {
            get => _ponderation;
            set => _ponderation = value;
        }

        private bool _isRenaming = false;
        public bool IsRenaming
        {
            get => _isRenaming;
            set => SetAndNotify(ref _isRenaming, value);
        }

        private bool _isExpanded = false;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetAndNotify(ref _isExpanded, value);
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndNotify(ref _isSelected, value);
        }

        private string _path;
        public string Path
        {
            get => _path;
            set
            {
                SetAndNotify(ref _path, value);
                Notify(nameof(FileExist));
            }
        }

        private bool _fileExist;
        public bool FileExist
        {
            get => File.Exists(Path);
            set => SetAndNotify(ref _fileExist, value);
        }
    }
}