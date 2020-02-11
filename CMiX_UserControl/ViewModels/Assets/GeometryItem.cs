using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;
using System.Linq.Dynamic;

namespace CMiX.Studio.ViewModels
{
    public class GeometryItem : ViewModel, IAssets
    {
        public GeometryItem(string name, string path)
        {
            Name = name;
            Path = path;
            Ponderation = ItemPonderation.AssetsPonderation;
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
            throw new System.NotImplementedException();
        }

        public void RemoveAsset()
        {
            throw new System.NotImplementedException();
        }

        public void Rename()
        {
            this.IsRenaming = true;
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
    }
}
