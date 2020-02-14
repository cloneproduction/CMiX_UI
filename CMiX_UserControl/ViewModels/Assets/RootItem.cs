using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Resources;
using System.Collections.Specialized;
using System.ComponentModel;

namespace CMiX.Studio.ViewModels
{
    public class RootItem : ViewModel, IDirectory
    {
        public RootItem()
        {
            Assets = new SortableObservableCollection<IAssets>();
            Assets.CollectionChanged += CollectionChanged;
            AddNewDirectoryCommand = new RelayCommand(p => AddNewDirectory());
        }

        public ICommand AddNewDirectoryCommand { get; set; }
        public ICommand RenameCommand { get; set; }
        public ICommand RemoveAssetCommand { get; set; }

        public SortableObservableCollection<IAssets> Assets { get; set; }

        private string _ponderation = "_";
        public string Ponderation
        {
            get => _ponderation;
            set => _ponderation = value;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private bool _isRoot = false;
        public bool IsRoot
        {
            get => _isRoot;
            set => SetAndNotify(ref _isRoot, value);
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

        public void AddNewDirectory()
        {
            var dir = new DirectoryItem("NewFolder");
            AddAsset(dir);
        }

        public void AddAsset(IAssets asset)
        {
            Assets.Add(asset);
            SortAssets();
        }

        public void RemoveAsset(IAssets asset)
        {
            if (Assets.Contains(asset))
                Assets.Remove(asset);
        }

        public void Rename()
        {
            throw new System.NotImplementedException();
        }

        public void SortAssets()
        {
            this.Assets.Sort(c => c.Name);
            this.Assets.Sort(c => c.Ponderation);
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
                SortAssets();
        }
    }
}
