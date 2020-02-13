using CMiX.MVVM.Resources;
using CMiX.MVVM.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic;
using System.Windows.Data;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class DirectoryItem : ViewModel, IAssets
    {
        public DirectoryItem(string name, string path, IAssets parentAsset)
        {
            Assets = new SortableObservableCollection<IAssets>();
            Assets.CollectionChanged += CollectionChanged;

            ParentAsset = parentAsset;
            IsExpanded = false;
            Name = name;
            Path = path;

            AddAssetCommand = new RelayCommand(p => AddAsset());
            RenameCommand = new RelayCommand(p => Rename());
        }

        public ICommand AddAssetCommand { get; set; }
        public ICommand RenameCommand { get; set; }
        public ICommand RemoveAssetCommand { get; set; }

        public SortableObservableCollection<IAssets> Assets { get; set; }

        public IAssets ParentAsset { get; set; }

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

        private string _ponderation = "a";
        public string Ponderation
        {
            get => _ponderation;
            set => _ponderation = value;
        }

        //public Enum Ponderation { get; set; }

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
            var directoryItem = new DirectoryItem("NewFolder", null, this);
            Assets.Add(directoryItem);
        }

        public void RemoveAsset()
        {
            throw new System.NotImplementedException();
        }

        public void Rename()
        {
            IsRenaming = true;
        }

        public void SortAssets()
        {
            Assets.Sort(c => c.Name);
            Assets.Sort(c => c.Ponderation.ToString());
        }

        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= item_PropertyChanged;
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += item_PropertyChanged;
            }
        }

        public void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Name))
            {
                SortAssets();
            }
        }
    }
}
