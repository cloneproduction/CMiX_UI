using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class DirectoryItem : ViewModel, IAssets
    {
        public DirectoryItem(string name, string path)
        {
            Assets = new ObservableCollection<IAssets>();
            IsExpanded = false;
            Name = name;
            Path = path;

            AddAssetCommand = new RelayCommand(p => AddAsset());
            RenameCommand = new RelayCommand(p => Rename());
        }

        public ICommand AddAssetCommand { get; set; }
        public ICommand RenameCommand { get; set; }
        public ICommand RemoveAssetCommand { get; set; }

        public ObservableCollection<IAssets> Assets { get; set; }

        private string _path;
        public string Path
        {
            get => _path;
            set => SetAndNotify(ref _path, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
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

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndNotify(ref _isSelected, value);
        }
        

        public void AddAsset()
        {
            IAssets directoryItem = new DirectoryItem("NewFolder", null);
            Assets.Add(directoryItem);
        }

        public void RemoveAsset()
        {
            throw new System.NotImplementedException();
        }

        public void Rename()
        {
            this.IsRenaming = true;
        }
    }
}
