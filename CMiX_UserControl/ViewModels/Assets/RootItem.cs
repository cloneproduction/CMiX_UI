using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public class RootItem : ViewModel, IAssets
    {
        public RootItem()
        {
            Ponderation = ItemPonderation.AssetsPonderation;

            Assets = new ObservableCollection<IAssets>();
            AddAssetCommand = new RelayCommand(p => AddAsset());
        }

        public ICommand AddAssetCommand { get; set; }
        public ICommand RenameCommand { get; set; }
        public ICommand RemoveAssetCommand { get; set; }

        public ObservableCollection<IAssets> Assets { get; set; }

        public Enum Ponderation { get; set; }

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

        private bool _isExpanded = true;
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

        public ObservableCollection<IAssets> ReorderAssets(ObservableCollection<IAssets> parentAsset)
        {
            Console.WriteLine("ReorderAssets");
            ObservableCollection<IAssets> temp;
            temp = new ObservableCollection<IAssets>(parentAsset.OrderBy($"{nameof(IAssets.Ponderation)}, {nameof(IAssets.Name)}"));
            parentAsset.Clear();
            foreach (IAssets j in temp) parentAsset.Add(j);
            return parentAsset;
        }

        public void AddAsset()
        {
            Assets.Add(new DirectoryItem("NewFolder", null, this));
        }

        public void RemoveAsset()
        {
            throw new System.NotImplementedException();
        }

        public void Rename()
        {
            throw new System.NotImplementedException();
        }
    }
}
