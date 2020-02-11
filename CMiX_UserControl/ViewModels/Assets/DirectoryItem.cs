using CMiX.MVVM.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq.Dynamic;
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
            Ponderation = ItemPonderation.DirectoryPonderation;

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

        public Enum Ponderation { get; set; }

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

        public ObservableCollection<IAssets> OrderThoseGroups(ObservableCollection<IAssets> orderThoseGroups)
        {
            ObservableCollection<IAssets> temp;
            temp = new ObservableCollection<IAssets>(orderThoseGroups.OrderBy($"{nameof(IAssets.Ponderation)}, {nameof(IAssets.Name)}"));
            orderThoseGroups.Clear();
            foreach (IAssets j in temp) orderThoseGroups.Add(j);
            Console.WriteLine("ORDER!");
            return orderThoseGroups;
        }

        public void AddAsset()
        {
            Console.WriteLine("AddAsset");
            IAssets directoryItem = new DirectoryItem("NewFolder", null);
            Assets.Add(directoryItem);
            OrderThoseGroups(this.Assets);
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
